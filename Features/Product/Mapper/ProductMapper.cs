using StoreProject.Common;
using StoreProject.Entities;
using StoreProject.Features.Product.DTOs;
using StoreProject.Features.Product.Model;
using StoreProject.Features.Shared.DTOs;

namespace StoreProject.Features.Product.Mapper
{
    public class ProductMapper
    {
        public static Entities.Product MapProductCreateDtoToProduct(ProductCreateDto productCreateDto)
        {
            return new Entities.Product()
            {
                CategoryId = productCreateDto.CategoryId,
                Title = productCreateDto.Title,
                Slug = productCreateDto.Slug.ToSlug(),
                CreationDate = DateTime.Now,
                Description = productCreateDto.Description,
                ImageUrl = productCreateDto.ImageUrl,
                Price = productCreateDto.Price,
                Stock = productCreateDto.Stock,
                IsDeleted = false,
            };
        }
        public static CategoryInfoDto MapCategoryToCategoryInfoDto(Entities.Category category)
        {
            return new CategoryInfoDto()
            {
                Id = category.Id,
                Title = category.Title,
                Slug = category.Slug
            };
        }
        public static ProductDto MapProductToProductDto(Entities.Product product)
        {
            return new ProductDto()
            {
                Id = product.Id,
                Title = product.Title,
                Slug = product.Slug,
                CreationDate = product.CreationDate,
                Description = product.Description,
                ImageUrl= product.ImageUrl,
                Price = product.Price,
                Stock = product.Stock,
                IsDeleted = product.IsDeleted,
                Category = MapCategoryToCategoryInfoDto(product.Category)
            };
        }
        public static Entities.Product MapProductEditDtoToProduct(Entities.Product product, ProductEditDto productEditDto)
        {
            product.CategoryId = productEditDto.CategoryId;
            product.Title = productEditDto.Title;
            product.Slug = productEditDto.Slug.ToSlug();
            product.Description = productEditDto.Description;
            product.ImageUrl = productEditDto.ImageUrl;
            product.Price = productEditDto.Price;
            product.Stock = productEditDto.Stock;
            product.IsDeleted = productEditDto.IsDeleted;

            return product;
        }
        public static ProductsForCategoryModel MapProductDtoToProductsForCategoryModel(ProductDto product)
        {
            return new ProductsForCategoryModel()
            {
                Title = product.Title,
                Slug = product.Slug,
                Price = product.Price,
                ImageUrl = product.ImageUrl
            };
        }
    }
}
