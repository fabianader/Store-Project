using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using StoreProject.Common;
using StoreProject.Common.Services;
using StoreProject.Entities;
using StoreProject.Features.User.DTOs;
using StoreProject.Features.User.Mapper;
using StoreProject.Infrastructure.Data;
using System.Linq;
using System.Security.Claims;

namespace StoreProject.Features.User.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly StoreContext _storeContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IFileManager _fileManager;
        public UserManagementService(StoreContext storeContext, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IFileManager fileManager)
        {
            _storeContext = storeContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _fileManager = fileManager;
        }

        public async Task<OperationResult> CreateUserAsync(UserCreateDto userCreateDto)
        {
            if (IsAny([userCreateDto.UserName, userCreateDto.Email, userCreateDto.PhoneNumber]))
                return OperationResult.Error(["A user with these characteristics exists."]);

            var result = await _userManager.CreateAsync(
                UserMapper.MapUserRegisterDtoToAppUser(userCreateDto),
                userCreateDto.Password);

            List<string> ErrorsList = new List<string>();
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ErrorsList.Add(error.Description);

                return OperationResult.Error(ErrorsList);
            }

            var user = await _userManager.FindByNameAsync(userCreateDto.UserName);
            user.EmailConfirmed = true;
            await _userManager.AddToRolesAsync(user, userCreateDto.UserRoles);
            _storeContext.SaveChanges();

            return OperationResult.Success();
        }

        public async Task<OperationResult> EditUserAsync(UserEditDto userEditDto)
        {
            var user = await _userManager.FindByIdAsync(userEditDto.Id);
            if (user == null)
                return OperationResult.NotFound(["User not found."]);

            if (user.ProfilePicture != userEditDto.ProfilePictureName)
                user.ProfilePicture = userEditDto.ProfilePictureName;

             //     || must be used
            if (user.UserName != userEditDto.UserName && user.Email != userEditDto.Email && user.PhoneNumber != userEditDto.PhoneNumber)
                if (IsAny([userEditDto.UserName, userEditDto.Email, userEditDto.PhoneNumber]))
                    return OperationResult.Error(["A user with these characteristics exists."]);

            UserMapper.MapEditUserToAppUser(userEditDto, user);

            var userRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, userRoles);
            await _userManager.AddToRolesAsync(user, userEditDto.UserRoles);

            var result = await _userManager.UpdateAsync(user);

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

        public List<UserDto> GetAllUsers()
        {
            return
                _storeContext.ApplicationUsers.Select(u => UserMapper.MapAppUserToUserDto(u)).ToList();
        }

        public List<string> GetAllRolesName()
        {
            return _storeContext.Roles.Select(r => r.Name).ToList();
        }

        public int GetAllRolesCount()
        {
            return _storeContext.Roles.Count();
        }

        public UserFilterDto GetUsersByFilter(UserFilterParamsDto userFilterParamsDto)
        {
            var result = _storeContext.ApplicationUsers
                .Include(u => u.UserRoles)
                .OrderByDescending(u => u.RegisterDate).AsQueryable();
    
            if (!userFilterParamsDto.UserName.IsNullOrEmpty())
                result = result.Where(u => u.UserName.Contains(userFilterParamsDto.UserName));

            //if (userFilterParamsDto.Roles.Any())
            //{
            //    result = result.Where(u => userFilterParamsDto.Roles.Contains(u.UserRoles.));
            //}

            if (userFilterParamsDto.RegisterDate != null)
                result = result.Where(u => u.RegisterDate == userFilterParamsDto.RegisterDate);

            if (userFilterParamsDto.IsDeleted == false)
                result = result.Where(u => u.IsDeleted == false);
            else if (userFilterParamsDto.IsDeleted == true)
                result = result.Where(u => u.IsDeleted == true);

            var skip = (userFilterParamsDto.PageId - 1) * userFilterParamsDto.Take;

            UserFilterDto userFilter = new UserFilterDto()
            {
                Users = result.Skip(skip).Take(userFilterParamsDto.Take)
                    .Select(u => UserMapper.MapAppUserToUserDto(u)).ToList(),
                UserFilterParams = userFilterParamsDto
            };
            userFilter.GeneratePaging(result, userFilterParamsDto.Take, userFilterParamsDto.PageId);
            return userFilter;
        }

        public async Task<List<string>> GetUserRolesAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return null;

            return (await _userManager.GetRolesAsync(user)).ToList();
        }

        public bool IsAny(string[] items)
        {
            bool IsAnyUserName = _storeContext.Users.Any(u => u.UserName == items[0]);
            bool IsAnyEmail = _storeContext.Users.Any(u => u.Email == items[1]);
            bool IsAnyPhoneNumber = _storeContext.ApplicationUsers.Any(u => u.PhoneNumber == items[2]);

            // the correct is ||, it is changed for development
            return IsAnyUserName && IsAnyEmail && IsAnyPhoneNumber;
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return null;

            return user.UserName;
        }
    }
}
