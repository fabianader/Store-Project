using Microsoft.AspNetCore.Mvc;
using StoreProject.Common;
using StoreProject.Common.Services;
using StoreProject.Features.Admin.DTOs;
using StoreProject.Features.Admin.Model;
using StoreProject.Features.Admin.Services;
using StoreProject.Features.User.DTOs;
using StoreProject.Features.User.Model;

namespace StoreProject.Features.Admin.Controllers
{
    [Route("Admin/Users/{action=index}")]
    public class UsersController : BaseController
    {
        private readonly IUserManagementService _userManagementService;
        private readonly IFileManager _fileManager;
        public UsersController(IUserManagementService userManagementService, IFileManager fileManager)
        {
            _userManagementService = userManagementService;
            _fileManager = fileManager;
        }

        public IActionResult Index(DateTime? registerDate, int pageId = 1, string username = "", List<string>? roles = null, bool isDeleted = false)
        {
            var parameters = new UserFilterParamsDto()
            {
                PageId = pageId,
                Take = 5,
                UserName = username,
                Roles = roles,
                RegisterDate = registerDate,
                IsDeleted = isDeleted
            };
            var model = _userManagementService.GetUsersByFilter(parameters);

            return View(model);
        }

        public IActionResult CreateUser()
        {
            return PartialView("_AdminCreateUser");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(UserCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                ErrorAlert();
                return RedirectToAction("Index");
            }

            var result = await _userManagementService.CreateUserAsync(new UserCreateDto()
            {
                UserName = model.UserName,
                FullName = model.FullName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserRoles = model.UserRoles,
                Password = model.Password
            });

            if (result.Status != OperationResultStatus.Success)
            {
                ErrorAlert(result.Message);
                return RedirectToAction("Index");
            }

            return RedirectAndShowAlert(result, RedirectToAction("Index"));
        }
        
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManagementService.GetUserByIdAsync(id);
            var model = new UserEditModel()
            {
                Id = id,
                UserName = user.UserName,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserRoles = await _userManagementService.GetUserRolesAsync(id),
                ProfilePictureName = user.ProfilePicture,
                IsDeleted = user.IsDeleted,
            };

            return PartialView("_AdminEditUser", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(UserEditModel model)
        {
            if (!ModelState.IsValid)
                return RedirectAndShowAlert(OperationResult.Error(), RedirectToAction("Index"));

            var result = await _userManagementService.EditUserAsync(new UserEditDto()
            {
                Id = model.Id,
                UserName = model.UserName,
                FullName = model.FullName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                ProfilePictureName = (model.ProfilePictureFile != null) ? _fileManager.SaveImageAndReturnImageName(model.ProfilePictureFile, Directories.DefaultUserProfilePicture) : model.ProfilePictureName,
                ProfilePicture = model.ProfilePictureFile,
                UserRoles = model.UserRoles,
                IsDeleted = model.IsDeleted,
            });

            if(result.Status != OperationResultStatus.Success)
            {
                ErrorAlert(result.Message);
                return RedirectToAction("Index");
            }

            return RedirectAndShowAlert(result, RedirectToAction("Index"));
        }
    }
}
