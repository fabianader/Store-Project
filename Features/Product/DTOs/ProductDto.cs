using StoreProject.Features.Category.DTOs;
using StoreProject.Features.Shared.DTOs;

namespace StoreProject.Features.Product.DTOs
{
    public class ProductDto : ProductBaseDto
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string ImageUrl { get; set; }
        public bool IsDeleted { get; set; }

        public CategoryInfoDto Category { get; set; }
    }
}
