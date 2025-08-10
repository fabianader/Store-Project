using StoreProject.Common;
using StoreProject.Features.Category.DTOs;

namespace StoreProject.Features.Shared
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
        OperationResult CreateCategory(CreateCategoryDto createCategoryDto);
        OperationResult EditCategory(EditCategoryDto editCategoryDto);
    }
}
