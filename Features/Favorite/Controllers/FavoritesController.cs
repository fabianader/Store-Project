using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreProject.Common;
using StoreProject.Features.Favorite.Mapper;
using StoreProject.Features.Favorite.Services;
using System.Security.Claims;

namespace StoreProject.Features.Favorite.Controllers
{
    [Authorize]
    [Route("UserPanel/Favorites/{action=index}")]
    public class FavoritesController : BaseController
    {
        private readonly IFavoriteService _favoriteService;
        public FavoritesController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return RedirectAndShowMessage("info", "User not found!");

            var favoritesDto = _favoriteService.GetFavorites(userId);
            var model = favoritesDto.Select(FavoriteMapper.MapFavoriteDtoToFavoritesModel).ToList();
            return View(model);
        }

        public IActionResult DeleteFromFavorites(int productId, string callBackUrl)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return RedirectAndShowMessage("info", "User not found!");

            var result = _favoriteService.DeleteFromFavorites(userId, productId);
            if (result.Status != OperationResultStatus.Success)
                return View();
            
            return Redirect(callBackUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleFavorite(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(userId == null)
                return RedirectAndShowMessage("info", "User not found!");

            bool exists = _favoriteService.IsFavorite(userId, productId);
            if (exists)
            {
                var result = _favoriteService.DeleteFromFavorites(userId, productId);
                if(result.Status != OperationResultStatus.Success)
                    return Json(new {added = true});

                return Json(new { added = false });
            }
            else
            {
                var result = _favoriteService.AddToFavorites(userId, productId);
                if(result.Status != OperationResultStatus.Success)
                    return Json(new {added = false});

                return Json(new { added = true });
            }
        }
    }
}
