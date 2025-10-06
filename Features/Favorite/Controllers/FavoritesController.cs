using Microsoft.AspNetCore.Mvc;
using StoreProject.Common;
using StoreProject.Features.Favorite.Mapper;
using StoreProject.Features.Favorite.Services;
using System.Security.Claims;

namespace StoreProject.Features.Favorite.Controllers
{
    public class FavoritesController : Controller
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
                return NotFound();

            var favoritesDto = _favoriteService.GetFavorites(userId);
            var model = favoritesDto.Select(FavoriteMapper.MapFavoriteDtoToFavoritesModel).ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return NotFound();

            var result = _favoriteService.AddToFavorites(userId, productId);
            if (result.Status != OperationResultStatus.Success)
                return BadRequest();

            return View();
        }

        [HttpPost]
        public IActionResult Delete(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return NotFound();

            var result = _favoriteService.DeleteFromFavorites(userId, productId);
            if (result.Status != OperationResultStatus.Success)
                return BadRequest();

            return View();
        }
    }
}
