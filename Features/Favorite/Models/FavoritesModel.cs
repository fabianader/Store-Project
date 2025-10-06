namespace StoreProject.Features.Favorite.Models
{
    public class FavoritesModel
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
    }
}
