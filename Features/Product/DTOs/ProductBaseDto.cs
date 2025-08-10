namespace StoreProject.Features.Product.DTOs
{
    public class ProductBaseDto
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
