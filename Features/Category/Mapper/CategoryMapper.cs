using StoreProject.Common;
using StoreProject.Features.Category.DTOs;
using StoreProject.Features.Category.Model;
using StoreProject.Features.Product.DTOs;
using StoreProject.Features.Product.Mapper;

namespace StoreProject.Features.Category.Mapper
{
    public class CategoryMapper
    {
        public static Entities.Category MapCreateCategoryDtoToCategory(CreateCategoryDto createCategoryDto)
        {
            return new Entities.Category()
            {
                ParentId = createCategoryDto.ParentId,
                Title = createCategoryDto.Title,
                Slug = createCategoryDto.Slug.ToSlug(),
                CreationDate = DateTime.Now,
                IsDeleted = false,
            };
        }

        public static CategoryDto MapCategoryToCategoryDto(Entities.Category category)
        {
            return new CategoryDto()
            {
                Id = category.Id,
                ParentId = category.ParentId,
                Title = category.Title,
                Slug = category.Slug,
                CreationDate = category.CreationDate,
                IsDeleted = category.IsDeleted,
            };
        }

        public static Entities.Category MapEditCategoryDtoToCategory(Entities.Category category, EditCategoryDto editCategoryDto)
        {
            category.ParentId = editCategoryDto.ParentId;
            category.Title = editCategoryDto.Title;
            category.Slug = editCategoryDto.Slug.ToSlug();
            category.IsDeleted = editCategoryDto.IsDeleted;
            
            return category;
        }

        public static CategoryModel MapCategoryDtoToCategoryModel(CategoryDto categoryDto)
        {
            return new CategoryModel()
            {
                Id = categoryDto.Id,
                Title = categoryDto.Title,
                Slug = categoryDto.Slug,
                CreationDate = categoryDto.CreationDate,
                ParentId = categoryDto.ParentId,
                IsDeleted = categoryDto.IsDeleted
            };
        }

        //public static DetailsModel MapCategoryDtoToDetailModel(CategoryDto category)
        //{
        //    return new DetailsModel()
        //    {
        //        Title = category.Title,
                
        //    };
        //}
    }
}
