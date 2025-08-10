namespace StoreProject.Features.Product.DTOs
{
    public class ProductEditDto : ProductBaseDto
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
