using StoreProject.Entities;
using StoreProject.Features.Favorite.DTOs;
using StoreProject.Features.Favorite.Models;

namespace StoreProject.Features.Favorite.Mapper
{
    public class FavoriteMapper
    {
        public static FavoriteDto MapFavoriteToFavoriteDto(Entities.Favorite favorite)
        {
            return new FavoriteDto()
            {
                ProductId = favorite.ProductId,
                ProductTitle = favorite.Product.Title,
                ProductSlug = favorite.Product.Slug,
                ImageUrl = favorite.Product.ImageUrl,
                Price = favorite.Product.Price
            };
        }

        public static FavoritesModel MapFavoriteDtoToFavoritesModel(FavoriteDto favoriteDto)
        {
            return new FavoritesModel()
            {
                ProductId = favoriteDto.ProductId,
                Title = favoriteDto.ProductTitle,
                Slug = favoriteDto.ProductSlug,
                ImageUrl = favoriteDto.ImageUrl,
                Price = favoriteDto.Price
            };
        }
    }
}
