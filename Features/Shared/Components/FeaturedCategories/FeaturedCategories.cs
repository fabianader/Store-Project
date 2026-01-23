using Microsoft.AspNetCore.Mvc;
using StoreProject.Features.Category.DTOs;
using StoreProject.Features.Category.Services;
using StoreProject.Features.Product.Services;
using StoreProject.Features.Shared.DTOs;
using StoreProject.Infrastructure.Data;

namespace StoreProject.Features.Shared.Components.FeaturedCategories
{
    public class FeaturedCategories : ViewComponent
    {
        private readonly ICategoryManagementService _categoryManagementService;
        public FeaturedCategories(ICategoryManagementService categoryManagementService)
        {
            _categoryManagementService = categoryManagementService;
        }

        public IViewComponentResult Invoke(int count)
        {
            var categories = _categoryManagementService
                .GetCategoriesWithHighestProductsCount(count);

            return View("FeaturedCategories", categories);
        }
    }
}
