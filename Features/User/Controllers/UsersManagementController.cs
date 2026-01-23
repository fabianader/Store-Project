using Microsoft.AspNetCore.Mvc;
using StoreProject.Common;
using StoreProject.Common.Services;
using StoreProject.Features.User.DTOs;
using StoreProject.Features.User.Model;
using StoreProject.Features.User.Services;

namespace StoreProject.Features.User.Controllers
{
    [Route("Admin/UsersManagement/{action=index}")]
    public class UsersManagementController : BaseController
    {
        private readonly IUserManagementService _userManagementService;
        private readonly IUserSharedService _userSharedService;
        private readonly IFileManager _fileManager;
        public UsersManagementController(IUserManagementService userManagementService, IFileManager fileManager, IUserSharedService userSharedService)
        {
            _userManagementService = userManagementService;
            _fileManager = fileManager;
            _userSharedService = userSharedService;
        }

        public IActionResult Index(DateTime? registerDate, int pageId = 1, string username = "",
                List<string>? roles = null, bool isDeleted = false)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string queryValue)
        {
            string url = $"{Request.Path}";

            string[] queryStringKeyValues = Request.QueryString.Value.Replace("?", string.Empty).Split('&');
            if (Request.QueryString.Value.Contains("username"))
            {
                url += "?";
                for (int i = 0; i < queryStringKeyValues.Length; i++)
                {
                    if (queryStringKeyValues[i].Contains("username"))
                    {
                        queryStringKeyValues[i] = $"username={queryValue}";
                    }

                    url += (i!= queryStringKeyValues.Length - 1) ? $"{queryStringKeyValues[i]}&" : queryStringKeyValues[i];
                }
            }
            else
            {
                url += Request.QueryString.Add("username", queryValue);
            }

            return Redirect(url);
        }

        public IActionResult CreateUser()
        {
            return PartialView("_AdminCreateUser");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(UserCreateModel model)
        {
            ViewBag.Layout = string.Empty;

            if (!ModelState.IsValid)
            {
                ViewBag.Layout = "_AdminLayout";
                return PartialViewAndShowErrorAlert(PartialView("_AdminCreateUser", model));
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
                ViewBag.Layout = "_AdminLayout";
                return PartialViewAndShowErrorAlert(PartialView("_AdminCreateUser", model), result.Message);
            }

            return RedirectAndShowAlert(result, RedirectToAction("Index"));
        }

        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userSharedService.GetUserByIdAsync(id);
            if (user == null)
                return RedirectAndShowAlert(OperationResult.NotFound(), RedirectToAction("Index"));

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
            var ProfilePicName = model.ProfilePictureName;
            ViewBag.Layout = string.Empty;

            if (!ModelState.IsValid)
            {
                ViewBag.Layout = "_AdminLayout";
                return PartialViewAndShowErrorAlert(PartialView("_AdminEditUser", model));
            }

            if (model.ProfilePictureFile != null)
                try
                {
                    ProfilePicName = _fileManager.SaveImageAndReturnImageName(model.ProfilePictureFile, Directories.UserProfilePicture);
                    model.ProfilePictureName = ProfilePicName;
                }
                catch
                {
                    ViewBag.Layout = "_AdminLayout";
                    return PartialViewAndShowErrorAlert(PartialView("_AdminEditUser", model), ["To change the profile picture, you must upload a photo."]);
                }



            var result = await _userManagementService.EditUserAsync(new UserEditDto()
            {
                Id = model.Id,
                UserName = model.UserName,
                FullName = model.FullName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                ProfilePictureName = model.ProfilePictureName,
                ProfilePicture = model.ProfilePictureFile,
                UserRoles = model.UserRoles,
                IsDeleted = model.IsDeleted,
            });

            if (result.Status != OperationResultStatus.Success)
            {
                ViewBag.Layout = "_AdminLayout";
                return PartialViewAndShowErrorAlert(PartialView("_AdminEditUser", model), result.Message);
            }

            return RedirectAndShowAlert(result, RedirectToAction("Index"));
        }
    }
}
