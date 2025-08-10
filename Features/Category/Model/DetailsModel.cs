using StoreProject.Common;
using StoreProject.Features.Product.DTOs;
using StoreProject.Features.Product.Model;

namespace StoreProject.Features.Category.Model
{
    public class DetailsModel : BasePagination
    {
        public ProductFilterDto Filter { get; set; }
        public string Title { get; set; }
        public int ProductsCount { get; set; }
    }
}
