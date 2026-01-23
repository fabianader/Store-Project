using StoreProject.Common;
using StoreProject.Features.Category.DTOs;

namespace StoreProject.Features.Category.Services
{
    public interface ICategoryManagementService
    {
        bool IsAnySlug(string slug, int? currentCategoryId = null);
        bool IsAnyTitle(string title, int? currentCategoryId = null);
        List<CategoryDto> GetExistedCategories();
        public List<CategoryDto> GetDeletedCategories();
        List<CategoryDto> GetChildCategories(int? parentId);
        CategoryDto GetCategoryBy(int? id);
        CategoryDto GetCategoryBy(string slug);
        List<CategoryDto> GetCategoriesWithHighestProductsCount(int count);
        OperationResult CreateCategory(CreateCategoryDto createCategoryDto);
        OperationResult EditCategory(EditCategoryDto editCategoryDto);
    }
}
