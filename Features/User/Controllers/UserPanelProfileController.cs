using Microsoft.AspNetCore.Mvc;
using StoreProject.Common;
using StoreProject.Common.Services;
using StoreProject.Features.User.DTOs;
using StoreProject.Features.User.Mapper;
using StoreProject.Features.User.Model;
using StoreProject.Features.User.Services;
using System.Security.Claims;

namespace StoreProject.Features.User.Controllers
{
	[Route("UserPanel/Profile/{action=index}")]
	public class UserPanelProfileController : BaseController
	{
		private readonly IUserSharedService _userSharedService;
		private readonly IUserService _userService;
		private readonly IFileManager _fileManager;
		public UserPanelProfileController(IUserSharedService userSharedService, IUserService userService, IFileManager fileManager)
		{
			_userSharedService = userSharedService;
			_userService = userService;
			_fileManager = fileManager;
		}

		public async Task<IActionResult> Index()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (userId == null)
				return NotFound();

			var user = await _userSharedService.GetUserByIdAsync(userId);
			if (user == null)
				return NotFound();

			var model = UserMapper.MapUserDtoToUserPanelProfileModel(user);
			return View(model);
		}

		public async Task<IActionResult> EditProfile()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (userId == null)
				return NotFound();

			var user = await _userSharedService.GetUserByIdAsync(userId);
			if (user == null)
				return NotFound();

			var model = UserMapper.MapUserDtoToUserPanelEditProfileModel(user);
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditProfile(UserPanelEditProfileModel model)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (userId == null)
				return NotFound();

			if (!ModelState.IsValid)
				return View(model);

			if (model.ProfilePictureFile != null)
			{
				try
				{
					model.ProfilePictureName = _fileManager.SaveImageAndReturnImageName(model.ProfilePictureFile, Directories.UserProfilePicture);
				}
				catch
				{
					ErrorAlert(["To change the profile picture, you must upload a photo."]);
					return View(model);
				}

			}

			var userPanelEditDto = UserMapper.MapUserPanelEditProfileModelToUserPanelEditDto(model);
			userPanelEditDto.Id = userId;
			var result = await _userService.UserPanelEditAsync(userPanelEditDto);

			if(result.Status != OperationResultStatus.Success)
			{
				ErrorAlert(result.Message);
				return View(model);
			}

			return RedirectAndShowAlert(result, RedirectToAction("Index"));
		}


		public IActionResult ChangePassword()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(UserPanelChangePasswordModel model)
		{
			if (!ModelState.IsValid)
                return View(model);

			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (userId == null)
				return NotFound();

			var result = await _userService.UserPanelChangePasswordAsync(new UserPanelChangePasswordDto()
			{
				UserId = userId,
				CurrentPassword = model.CurrentPassword,
				NewPassword = model.NewPassword
			});

			if(result.Status != OperationResultStatus.Success)
			{
				ErrorAlert(result.Message);
				return View(model);
			}

			return RedirectAndShowAlert(result, RedirectToAction("Index"));
        }

		
    }
}
