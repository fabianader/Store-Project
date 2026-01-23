using StoreProject.Common;
using StoreProject.Features.Favorite.DTOs;

namespace StoreProject.Features.Favorite.Services
{
    public interface IFavoriteService
    {
        List<FavoriteDto> GetFavorites(string userId);
        OperationResult AddToFavorites(string userId, int productId);
        OperationResult DeleteFromFavorites(string userId,int productId);
        bool IsFavorite(string userId, int productId);
    }
}
