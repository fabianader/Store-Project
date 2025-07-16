using StoreProject.Common;
using StoreProject.Features.Category.DTOs;

namespace StoreProject.Features.Category.Services
{
    public interface ICategoryService
    {
        OperationResult GetAllCategories();
        CategoryDto GetCategoryBy(int id);
        CategoryDto GetCategoryBy(string slug);
        OperationResult CreateCategory(CreateCategoryDto createCategoryDto);
        OperationResult EditCategory(EditCategoryDto editCategoryDto);
    }
}
