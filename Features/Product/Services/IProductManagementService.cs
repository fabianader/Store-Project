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
        int GetProductStock(int productId);
        OperationResult CreateProduct(ProductCreateDto productCreateDto);
        OperationResult EditProduct(ProductEditDto productEditDto);
        bool DecreaseStock(int productId, int quantity);
        ProductDto GetProductBy(int id);
        ProductDto GetProductBy(string slug);
        string GetProductTitle(int productId);
        bool IsInStock(int id);
    }
}
