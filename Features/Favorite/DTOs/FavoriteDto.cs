namespace StoreProject.Features.Favorite.DTOs
{
    public class FavoriteDto
    {
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public string ProductSlug { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
    }
}
