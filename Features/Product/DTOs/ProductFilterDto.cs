using StoreProject.Common;

namespace StoreProject.Features.Product.DTOs
{
    public class ProductFilterDto : BasePagination
    {
        public List<ProductDto> Products { get; set; }
        public ProductFilterParamsDto ProductFilterParams { get; set; }
    }

    public class ProductFilterParamsDto
    {
        public int PageId { get; set; }
        public int Take { get; set; }

        public int? CategoryId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool IsDeleted { get; set; }
    }
}
