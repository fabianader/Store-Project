using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;
using StoreProject.Common;
using StoreProject.Features.User.DTOs;
using StoreProject.Features.User.Model;
using StoreProject.Features.User.Services;
using System.Security.Claims;

namespace StoreProject.Features.User.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "An Error Occurred...");
                return View(model);
            }

            var result = await _userService.UserRegister(new UserRegisterDto
            {
                UserName = model.UserName,
                FullName = model.FullName,
                Email = model.Email,
                Password = model.Password,
                PhoneNumber = model.PhoneNumber
            });

            if(result.Status != OperationResultStatus.Success)
            {
                foreach (var error in result.Message)
                    ModelState.AddModelError(string.Empty, error);
                return View(model);
            }

            return RedirectToAction("SixDigitEmailCode", new { model.UserName, calledByAction="Register"});
        }

        public IActionResult SixDigitEmailCode(string username, string calledByAction)
        {
            var model = new SixDigitEmailCodeModel()
            {
                UserName = username,
                CalledByAction = calledByAction
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SixDigitEmailCode(SixDigitEmailCodeModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Error");
                return View(model);
            }

            var result = await _userService.SixDigitCodeCheck(new SixDigitCodeCheckDto()
            {
                UserName = model.UserName,
                Code = model.Code,
            }, model.CalledByAction);

            if(result.Status != OperationResultStatus.Success)
            {
                foreach (var error in result.Message)
                    ModelState.AddModelError(string.Empty, error);
                return View(model);
            }

            string action = (model.CalledByAction == "Register") ? "Login" : "ResetPassword";
            string routeValues = (action == "ResetPassword") ? model.UserName : string.Empty;
            return RedirectToAction(action, new {username=routeValues});
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Error");
                return View(model);
            }

            var result = await _userService.UserLogin(new UserLoginDto()
            {
                UserName = model.UserName,
                Password = model.Password,
                RememberMe = model.RememberMe
            });

            if(result.Status != OperationResultStatus.Success)
            {
                foreach (var error in result.Message)
                    ModelState.AddModelError(string.Empty, error);
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _userService.UserLogout();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Error");
                return View(model);
            }

            var result = await _userService.UserForgetPassword(new UserForgetPasswordDto()
            {
                UserName = model.UserName,
                Email = model.Email,
                // Token = model.Token
            });
            
            if(result.Status != OperationResultStatus.Success)
            {
                foreach (var error in result.Message)
                    ModelState.AddModelError(string.Empty, error);
                return View(model);
            }

            return RedirectToAction("SixDigitEmailCode", new { model.UserName,  calledByAction="ForgetPassword" });
        }

        public IActionResult ResetPassword(string username, string token, string returnUrl=null)
        {

            ResetPasswordModel model = new ResetPasswordModel()
            {
                UserName = username,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(string username, ResetPasswordModel model, string returnUrl=null)
        {
            returnUrl = 
                (!returnUrl.IsNullOrEmpty() && Url.IsLocalUrl(returnUrl)) ? returnUrl : "/Auth/Login";

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Error");
                return View(model);
            }

            var result = await _userService.UserResetPassword(new UserResetPasswordDto()
            {
                UserName = username,
                NewPassword = model.NewPassword
            });

            if(result.Status != OperationResultStatus.Success)
            {
                foreach (var error in result.Message)
                    ModelState.AddModelError(string.Empty, error);
                return View(model);
            }

            
            return Redirect(returnUrl);
        }

    }
}
