using StoreProject.Common;
using StoreProject.Features.Product.DTOs;

namespace StoreProject.Features.Product.Services
{
    public interface IProductManagementService
    {
        bool IsAnySlug(string slug, int? currentProductId = null);
        bool IsAnyTitle(string title, int? currentProductId = null);
        List<ProductDto> GetAllProducts();
        ProductFilterDto GetProductsByFilter(ProductFilterParamsDto productFilterParamsDto);
        List<ProductDto> GetProductsByCategoryId(int categoryId);
        OperationResult CreateProduct(ProductCreateDto productCreateDto);
        OperationResult EditProduct(ProductEditDto productEditDto);
        ProductDto GetProductBy(int id);
        ProductDto GetProductBy(string slug);
        bool IsInStock(int id);
    }
}
