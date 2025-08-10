using Microsoft.AspNetCore.Mvc;
using StoreProject.Features.Category.Model;
using StoreProject.Features.Product.Services;
using StoreProject.Features.Shared;

namespace StoreProject.Features.Category.Views.Categories.Components.BrowseCategories
{
    public class BrowseCategories : ViewComponent
    {
        private readonly ICategoryManagementService _categoryManagementService;
        private readonly IProductManagementService _productManagementService;
        public BrowseCategories(ICategoryManagementService categoryManagementService, IProductManagementService productManagementService)
        {
            _categoryManagementService = categoryManagementService;
            _productManagementService = productManagementService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = _categoryManagementService.GetExistedCategories();
            var model = categories
                .Where(c => c.ParentId == null)
                .Select(c => new BrowseCategoriesModel()
            {
                Title = c.Title,
                Slug = c.Slug,
                ProductsCount = _productManagementService.GetProductsByCategoryId(c.Id).Count
            }).ToList();

            return await Task.FromResult(View("BrowseCategories", model));
        }
    }
}
