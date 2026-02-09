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
        int? GetProductSalesCount(int productId);
        int? GetProductFavoritesCount(int productId);
        List<ProductDto> GetBestSellingProducts(int count);
        List<ProductDto> GetMostFavoritedProducts(int count);
        List<ProductDto> SearchProductsByTitle(string productTitle);
        OperationResult IncreaseStock(int productId, int quantity);
        OperationResult DecreaseStock(int productId, int quantity);
		OperationResult CreateProduct(ProductCreateDto productCreateDto);
        OperationResult EditProduct(ProductEditDto productEditDto);
        bool ReserveQuantity(int productId, int quantity);
		OperationResult UnreserveQuantity(int productId, int quantity);
		ProductDto GetProductBy(int id);
        ProductDto GetProductBy(string slug);
        string GetProductTitle(int productId);
        bool IsInStock(int id);
    }
}
