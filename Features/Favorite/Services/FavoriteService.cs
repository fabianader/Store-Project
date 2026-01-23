using Microsoft.EntityFrameworkCore;
using StoreProject.Common;
using StoreProject.Features.Favorite.DTOs;
using StoreProject.Features.Favorite.Mapper;
using StoreProject.Infrastructure.Data;

namespace StoreProject.Features.Favorite.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly StoreContext _context;
        public FavoriteService(StoreContext context)
        {
            _context = context;
        }

        public List<FavoriteDto> GetFavorites(string userId)
        {
            var userFavorites = _context.Favorites
                .Where(f => f.UserId == userId)
                .Include(f => f.Product)
                .OrderByDescending(f => f.CreatedAt).ToList();

            var favoritesDto = userFavorites
                .Select(FavoriteMapper.MapFavoriteToFavoriteDto).ToList();

            return favoritesDto;
        }

        public OperationResult AddToFavorites(string userId, int productId)
        {
            var IsInUserFavorite = _context.Favorites
                .Any(f => f.UserId == userId && f.ProductId == productId);

            if (IsInUserFavorite)
                return OperationResult.Error(["The product was already added to favorites."]);

            _context.Favorites.Add(new Entities.Favorite()
            {
                UserId = userId,
                ProductId = productId,
                CreatedAt = DateTime.UtcNow
            });

            _context.SaveChanges();
            return OperationResult.Success();
        }

        public OperationResult DeleteFromFavorites(string userId, int productId)
        {
            var deletedCount = _context.Favorites
                .Where(f => f.UserId == userId && f.ProductId == productId).ExecuteDelete();
            
            if (deletedCount == 0)
                return OperationResult.NotFound(["Favorite product not found."]);

            return OperationResult.Success();
        }

        public bool IsFavorite(string userId, int productId)
        {
            return _context.Favorites
                .Any(f => f.UserId == userId && f.ProductId == productId);
        }
    }
}
