using Microsoft.AspNetCore.Mvc;
using StoreProject.Features.Category.Mapper;
using StoreProject.Features.Category.Services;

namespace StoreProject.Features.Category.Views.Categories.Components.CategoryTree
{
    public class CategoryTree : ViewComponent
    {
        private readonly ICategoryManagementService _categoryManagementService;
        public CategoryTree(ICategoryManagementService categoryManagementService)
        {
            _categoryManagementService = categoryManagementService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = _categoryManagementService.GetExistedCategories()
                .Where(category => category.ParentId == null)
                .Select(category => CategoryMapper.MapCategoryDtoToCategoryModel(category)).ToList();

            return await Task.FromResult(View("CategoryTree", model));
        }
    }
}
