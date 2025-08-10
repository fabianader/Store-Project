using StoreProject.Common;
using StoreProject.Features.Category.DTOs;
using StoreProject.Features.Category.Mapper;
using StoreProject.Features.Shared;
using StoreProject.Infrastructure.Data;

namespace StoreProject.Features.Category.Services
{
    public class CategoryManagementService : ICategoryManagementService
    {
        private readonly StoreContext _context;
        public CategoryManagementService(StoreContext context)
        {
            _context = context;
        }

        public OperationResult CreateCategory(CreateCategoryDto createCategoryDto)
        {
            if (IsAnySlug(createCategoryDto.Slug))
                return OperationResult.Error(["Slug already exists."]);

            if (IsAnyTitle(createCategoryDto.Title))
                return OperationResult.Error(["Title already exists."]);

            var category = (CategoryMapper.MapCreateCategoryDtoToCategory(createCategoryDto));
            _context.Categories.Add(category);
            _context.SaveChanges();

            return OperationResult.Success();
        }

        public OperationResult EditCategory(EditCategoryDto editCategoryDto)
        {
            var category = _context.Categories.FirstOrDefault(category => category.Id == editCategoryDto.Id);
            if (category == null)
                return OperationResult.NotFound(["Category not found."]);

            //if (editCategoryDto.Slug != null)
            //    if (IsAnySlug(editCategoryDto.Slug))
            //        return OperationResult.Error(["Slug already exists."]);

            if (editCategoryDto.Slug.ToSlug() != category.Slug)
                if (IsAnySlug(editCategoryDto.Slug, category.Id))
                    return OperationResult.Error(["Slug already exists."]);

            if (editCategoryDto.Title != category.Title)
                if (IsAnyTitle(editCategoryDto.Title, category.Id))
                    return OperationResult.Error(["Title already exists."]);

            CategoryMapper.MapEditCategoryDtoToCategory(category, editCategoryDto);
            _context.SaveChanges();

            return OperationResult.Success();
        }

        public List<CategoryDto> GetExistedCategories()
        {
            return _context.Categories
                .Where(category => category.IsDeleted == false)
                .Select(category => CategoryMapper.MapCategoryToCategoryDto(category)).ToList();
        }

        public List<CategoryDto> GetDeletedCategories()
        {
            return _context.Categories
                .Where(category => category.IsDeleted == true)
                .Select(category => CategoryMapper.MapCategoryToCategoryDto(category)).ToList();
        }

        public CategoryDto GetCategoryBy(int? id)
        {
            var category = _context.Categories.FirstOrDefault(category => category.Id == id);
            if (category == null)
                return null;

            return CategoryMapper.MapCategoryToCategoryDto(category);
        }

        

        public CategoryDto GetCategoryBy(string slug)
        {
            var category = _context.Categories.FirstOrDefault(category => category.Slug == slug);
            if (category == null)
                return null;

            return CategoryMapper.MapCategoryToCategoryDto(category);
        }

        public bool IsAnySlug(string slug, int? currentCategoryId = null)
        {
            return _context.Categories.Any(category => category.Slug == slug.ToSlug() && category.Id != currentCategoryId);
        }

        public bool IsAnyTitle(string title, int? currentCategoryId = null)
        {
            return _context.Categories.Any(category => category.Title == title && category.Id != currentCategoryId);
        }

        public List<CategoryDto> GetChildCategories(int? parentId)
        {
            if (parentId == null)
                return null;

            return _context.Categories
                .Where(category => category.ParentId == parentId && category.IsDeleted == false)
                .Select(category => CategoryMapper.MapCategoryToCategoryDto(category)).ToList();
        }
    }
}
