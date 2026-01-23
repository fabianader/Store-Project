using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using StoreProject.Common;
using StoreProject.Entities;
using StoreProject.Features.User.DTOs;
using StoreProject.Features.User.Mapper;
using StoreProject.Infrastructure.Data;
using System.Text;

namespace StoreProject.Features.User.Services
{
    public class UserService : IUserService
    {
        private readonly StoreContext _storeContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMemoryCache _memoryCache;

        public UserService(StoreContext storeContext,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, 
            SignInManager<ApplicationUser> signInManager, IEmailSender emailSender,
            IHttpContextAccessor contextAccessor, IMemoryCache memoryCache)
        {
            _storeContext = storeContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _contextAccessor = contextAccessor;
            _memoryCache = memoryCache;
        }

        public async Task<OperationResult> UserRegister(UserRegisterDto userRegisterDto)
        {
            if (userRegisterDto == null)
                return OperationResult.NotFound();

            if (IsAny([userRegisterDto.UserName, userRegisterDto.Email, userRegisterDto.PhoneNumber]))
                return OperationResult.Error(["A user with these characteristics exists."]);

            var result = await _userManager.CreateAsync(
                UserMapper.MapUserRegisterDtoToAppUser(userRegisterDto),
                userRegisterDto.Password);

            List<string> ErrorsList = new List<string>();
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ErrorsList.Add(error.Description);
                }
                return OperationResult.Error(ErrorsList);
            }

            var user = await _userManager.FindByNameAsync(userRegisterDto.UserName);
            await _userManager.AddToRoleAsync(user, "User");

            _storeContext.SaveChanges();

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            token = Encrypt(token);
            _memoryCache.Set($"EmailConfirmationToken_{user.Id}", token, TimeSpan.FromMinutes(10));

            // Sends Authentication Code To User's Email
            var emailSendingResult = await SixDigitCodeEmailSender(user, user.Email);
            if(emailSendingResult.Status != OperationResultStatus.Success)
                return OperationResult.Error(["An error occurred in sending the code."]);
            
            return OperationResult.Success();
        }
        
        public async Task<OperationResult> UserLogin(UserLoginDto userLoginDto)
        {
            var user = await _userManager.FindByNameAsync(userLoginDto.UserName);
            if (user == null)
                return OperationResult.NotFound(["User not found."]);

            var result = await _signInManager
                .PasswordSignInAsync(user, userLoginDto.Password, userLoginDto.RememberMe, false);

            if (result.IsNotAllowed)
                return OperationResult.Error(["User not allowed"]);

            if (result.Succeeded)
                return OperationResult.Success();

            return OperationResult.Error(["An error occurred."]);
        }

        public async Task<OperationResult> UserLogout()
        {
            await _signInManager.SignOutAsync();
            return OperationResult.Success();
        }


        public bool IsAny(string[] items)
        {
           bool IsAnyUserName = _storeContext.Users.Any(u => u.UserName == items[0]);
           bool IsAnyEmail = _storeContext.Users.Any(u => u.Email == items[1]);
           bool IsAnyPhoneNumber = _storeContext.ApplicationUsers.Any(u => u.PhoneNumber == items[2]);

           // the correct is ||, it is changed for development
           return IsAnyUserName && IsAnyEmail && IsAnyPhoneNumber; 
        }

        public string Encrypt(string token)
        {
            return WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        }

        public string Decrypt(string token)
        {
            return Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
        }

        private async Task<OperationResult> SixDigitCodeEmailSender(ApplicationUser user, string email)
        {
            if (user == null)
                return OperationResult.NotFound(["User not found."]);

            if (user.Email != email)
                return OperationResult.Error(["Email is not valid"]);

            string sixDigitCode = await _userManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultPhoneProvider);
            await _emailSender.SendEmailAsync(new EmailModel(user.Email, "Authentication Code", sixDigitCode));
            return OperationResult.Success();
        }

        public async Task<OperationResult> SixDigitCodeCheck(SixDigitCodeCheckDto sixDigitCodeCheckDto, string forAction)
        {
            if (sixDigitCodeCheckDto == null)
                return OperationResult.NotFound();

            var user = await _userManager.FindByNameAsync(sixDigitCodeCheckDto.UserName);
            if (user == null)
                return OperationResult.NotFound(["User not found."]);

            bool CodeCheck = await _userManager.VerifyTwoFactorTokenAsync(user, TokenOptions.DefaultPhoneProvider, sixDigitCodeCheckDto.Code);
            if(CodeCheck == false)
                return OperationResult.Error(["Invalid code."]);

            if(forAction == "Register")
            {
                string? token = _memoryCache.Get<string>($"EmailConfirmationToken_{user.Id}");
                if (token.IsNullOrEmpty())
                    return OperationResult.Error();
                token = Decrypt(token);
                var result = await _userManager.ConfirmEmailAsync(user, token);

                if (result.Errors.Any())
                    return OperationResult.Error();
            }

            return OperationResult.Success();
        }


        public async Task<OperationResult> UserForgetPassword(UserForgetPasswordDto userForgetPasswordDto)
        {
            var user = await _userManager.FindByNameAsync(userForgetPasswordDto.UserName);
            if (user == null)
                return OperationResult.NotFound(["User not found."]);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            token = Encrypt(token);
            _memoryCache.Set($"PasswordResetToken_{user.Id}", token, TimeSpan.FromMinutes(10));

            var result = await SixDigitCodeEmailSender(user, userForgetPasswordDto.Email);
            if(result.Status != OperationResultStatus.Success)
                return OperationResult.Error(["An error occurred in sending the code."]);

            return OperationResult.Success();
        }

        public async Task<OperationResult> UserResetPassword(UserResetPasswordDto userResetPasswordDto)
        {
            var user = await _userManager.FindByNameAsync(userResetPasswordDto.UserName);
            if (user == null)
                return OperationResult.NotFound(["User not found."]);

            string? token = _memoryCache.Get<string>($"PasswordResetToken_{user.Id}");
            if (token.IsNullOrEmpty())
                return OperationResult.Error();
            token = Decrypt(token);
            var result = await _userManager.ResetPasswordAsync(user, token, userResetPasswordDto.NewPassword);
            
            List<string> ErrorsList = new List<string>();
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ErrorsList.Add(error.Description);

                return OperationResult.Error(ErrorsList);
            }

            return OperationResult.Success();
        }

		public async Task<OperationResult> UserPanelEditAsync(UserPanelEditDto userPanelEditDto)
		{
            var user = await _userManager.FindByIdAsync(userPanelEditDto.Id);
            if (user == null)
                return OperationResult.NotFound(["User not found."]);
			
            //     ||  must be used
            if (user.UserName != userPanelEditDto.UserName && user.Email != userPanelEditDto.Email && user.PhoneNumber != userPanelEditDto.PhoneNumber)
				if (IsAny([userPanelEditDto.UserName, userPanelEditDto.Email, userPanelEditDto.PhoneNumber]))
				    return OperationResult.Error(["A user with these characteristics exists."]);

            UserMapper.MapUserPanelEditDtoToAppUser(userPanelEditDto, user);

            var result = await _userManager.UpdateAsync(user);

			List<string> ErrorsList = new List<string>();
			if (!result.Succeeded)
            {
                foreach(var error in result.Errors)
                    ErrorsList.Add(error.Description);

                return OperationResult.Error(ErrorsList);
            }

			await _signInManager.RefreshSignInAsync(user);
			return OperationResult.Success();
		}

        public async Task<OperationResult> UserPanelChangePasswordAsync(UserPanelChangePasswordDto userPanelChangePasswordDto)
        {
            var user = await _userManager.FindByIdAsync(userPanelChangePasswordDto.UserId);
            if (user == null)
                return OperationResult.NotFound(["User not found."]);

            var result = await _userManager.ChangePasswordAsync(user, userPanelChangePasswordDto.CurrentPassword, userPanelChangePasswordDto.NewPassword);

            List<string> ErrorsList = new List<string>();
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ErrorsList.Add(error.Description);

                return OperationResult.Error(ErrorsList);
            }

            await _signInManager.RefreshSignInAsync(user);
            
            return OperationResult.Success();
        }
    }
}
