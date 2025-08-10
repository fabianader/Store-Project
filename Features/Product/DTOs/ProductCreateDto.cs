namespace StoreProject.Features.Product.DTOs
{
    public class ProductCreateDto : ProductBaseDto
    {
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
    }
}
