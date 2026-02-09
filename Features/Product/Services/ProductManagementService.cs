using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StoreProject.Common;
using StoreProject.Entities;
using StoreProject.Features.Category.Services;
using StoreProject.Features.Product.DTOs;
using StoreProject.Features.Product.Mapper;
using StoreProject.Infrastructure.Data;

namespace StoreProject.Features.Product.Services
{
    public class ProductManagementService : IProductManagementService
    {
        private readonly StoreContext _context;
        private readonly ICategoryManagementService _categoryManagementService;
        public ProductManagementService(StoreContext context, ICategoryManagementService categoryManagementService)
        {
            _context = context;
            _categoryManagementService = categoryManagementService;
        }

        public OperationResult CreateProduct(ProductCreateDto productCreateDto)
        {
            if (IsAnyTitle(productCreateDto.Title))
                return OperationResult.Error(["Title already exists."]);

            if (IsAnySlug(productCreateDto.Slug))
                return OperationResult.Error(["Slug already exists."]);

            var product = ProductMapper.MapProductCreateDtoToProduct(productCreateDto);
            _context.Products.Add(product);
            _context.SaveChanges();

            return OperationResult.Success();
        }

        public OperationResult EditProduct(ProductEditDto productEditDto)
        {
            var product = _context.Products.FirstOrDefault(product => product.Id == productEditDto.Id);
            if (product == null)
                return OperationResult.NotFound(["Product not found."]);

            if (productEditDto.Slug != product.Slug)
                if (IsAnySlug(productEditDto.Slug, product.Id))
                    return OperationResult.Error(["Slug already exists."]);

            if (productEditDto.Title != product.Title)
                if (IsAnyTitle(productEditDto.Title, product.Id))
                    return OperationResult.Error(["Title already exists."]);

            ProductMapper.MapProductEditDtoToProduct(product, productEditDto);
            _context.SaveChanges();

            return OperationResult.Success();
        }

        public bool ReserveQuantity(int productId, int quantity)
        {
            var affectedRows = _context.Products
                .Where(p => p.Id == productId && p.Stock >= p.ReservedQuantity + quantity)
                .ExecuteUpdate(p => p.SetProperty(x => x.ReservedQuantity, x => x.ReservedQuantity + quantity));

            return affectedRows > 0;
        }

        public OperationResult UnreserveQuantity(int productId, int quantity)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
                return OperationResult.NotFound(["Product not found."]);

			product.ReservedQuantity -= quantity;
            _context.SaveChanges();
            return OperationResult.Success();
		}

        public List<ProductDto> GetAllProducts()
        {
            return _context.Products
                .OrderByDescending(product => product.CreationDate)
                .Include(product => product.Category)
                .Select(product => ProductMapper.MapProductToProductDto(product)).ToList();
        }

        public ProductFilterDto GetProductsByFilter(ProductFilterParamsDto productFilterParamsDto)
        {
            var result = _context.Products
                .OrderByDescending(product => product.CreationDate)
                .Include(product => product.Category).AsQueryable();

            if (productFilterParamsDto.CategoryId != null)
            {
                List<int> CategoryIds = new();
                var ChildCategoriesIds = _categoryManagementService.GetChildCategories(productFilterParamsDto.CategoryId).Select(c => c.Id).ToList();

                if (ChildCategoriesIds.Count != 0)
                    CategoryIds.AddRange(ChildCategoriesIds);
                CategoryIds.Add((int)productFilterParamsDto.CategoryId);

                result = result.Where(product => CategoryIds.Contains(product.CategoryId));
            }
            
            if (!productFilterParamsDto.Title.IsNullOrEmpty())
                result = result.Where(product => product.Title.Contains(productFilterParamsDto.Title));

            if (!productFilterParamsDto.Slug.IsNullOrEmpty())
                result = result.Where(product => product.Slug == productFilterParamsDto.Slug.ToSlug());

            if(productFilterParamsDto.MinPrice.HasValue)
                result = result.Where(product => product.Price >=  productFilterParamsDto.MinPrice.Value);
            if (productFilterParamsDto.MaxPrice.HasValue)
                result = result.Where(product => product.Price <= productFilterParamsDto.MaxPrice.Value);


            if (productFilterParamsDto.IsDeleted == false)
                result = result.Where(product => product.IsDeleted == false);
            if (productFilterParamsDto.IsDeleted == true)
                result = result.Where(product => product.IsDeleted == true);

            var skip = (productFilterParamsDto.PageId - 1) * productFilterParamsDto.Take;
            ProductFilterDto productFilter = new()
            {
                Products = result.Skip(skip).Take(productFilterParamsDto.Take)
                    .Select(product => ProductMapper.MapProductToProductDto(product)).ToList(),
                ProductFilterParams = productFilterParamsDto
            };

            productFilter.GeneratePaging(result, productFilterParamsDto.Take, productFilterParamsDto.PageId);
            return productFilter;
        }

        public ProductDto GetProductBy(int id)
        {
            var product = _context.Products
                .Include(product => product.Category)
                .FirstOrDefault(product => product.Id == id);

            if (product == null)
                return null;

            var productDto = ProductMapper.MapProductToProductDto(product);
            return productDto;
        }

        public ProductDto GetProductBy(string slug)
        {
            var product = _context.Products
                .Include(product => product.Category)
                .FirstOrDefault(product => product.Slug == slug);

            if (product == null)
                return null;

            var productDto = ProductMapper.MapProductToProductDto(product);
            return productDto;
        }

        public List<ProductDto> GetProductsByCategoryId(int categoryId)
        {
            var products = _context.Products
                .Where(product => product.CategoryId == categoryId || product.Category.ParentId == categoryId)
                .OrderByDescending(product => product.CreationDate)
                .Select(product => ProductMapper.MapProductToProductDto(product)).ToList();

            return products;
        }

        public bool IsAnySlug(string slug, int? currentProductId = null)
        {
            return _context.Products.Any(product => product.Slug == slug && product.Id != currentProductId);
        }

        public bool IsAnyTitle(string title, int? currentProductId = null)
        {
            return _context.Products.Any(product => product.Title == title && product.Id != currentProductId);
        }

        public bool IsInStock(int id)
        {
            var product = _context.Products.FirstOrDefault(product => product.Id == id);
            if(product == null)
                return false;

            return (product.Stock > 0);
        }

        public string GetProductTitle(int productId)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
                return null;

            return product.Title;
        }
        
        public int GetProductStock(int productId)
        {
            var product = _context.Products.FirstOrDefault(product => product.Id == productId);
            if(product == null)
                return 0;

            return product.Stock;
        }

        public int? GetProductSalesCount(int productId)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
                return null;

            var salesCount = _context.OrderItems
                .Include(oi => oi.Order)
                .Where(oi => oi.ProductId == productId && 
                      (oi.Order.Status != OrderStatus.Cancelled && oi.Order.Status != OrderStatus.Pending))
                .Sum(oi => oi.Quantity);

            return salesCount;
        }

        public int? GetProductFavoritesCount(int productId)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
                return null;

            var favoritesCount = _context.Favorites.Count(f => f.ProductId == productId);
            return favoritesCount;
        }


        public List<ProductDto> GetBestSellingProducts(int count)
        {
            var products = _context.Products
                .Include(p => p.Category).ToList();

            var bestSellingProducts = products
                .OrderByDescending(p => GetProductSalesCount(p.Id))
                .Take(count)
                .Select(ProductMapper.MapProductToProductDto).ToList();

            return bestSellingProducts;
        }
        
        public List<ProductDto> GetMostFavoritedProducts(int count)
        {
            var products = _context.Products
                .Include(p => p.Category).ToList();

            var mostFavoritedProducts = products
                .OrderByDescending(p => GetProductFavoritesCount(p.Id))
                .Take(count)
                .Select(ProductMapper.MapProductToProductDto).ToList();

            return mostFavoritedProducts;
        }



        public OperationResult IncreaseStock(int productId, int quantity)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            if(product == null)
                return OperationResult.NotFound(["Product not found."]);

            product.Stock += quantity;
            _context.SaveChanges();
            return OperationResult.Success();
        }


        public OperationResult DecreaseStock(int productId, int quantity)
		{
            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
                return OperationResult.NotFound(["Product not found."]);

            product.Stock -= quantity;
            _context.SaveChanges();
            return OperationResult.Success();
		}

        public List<ProductDto> SearchProductsByTitle(string productTitle)
        {
            var products = _context.Products
                .Include(p => p.Category)
                .Where(p => p.Title.Contains(productTitle))
                .Select(ProductMapper.MapProductToProductDto).ToList();

            return products;
        }
    }
}
