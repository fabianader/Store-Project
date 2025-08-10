using Microsoft.AspNetCore.Mvc;
using StoreProject.Features.Category.Model;
using StoreProject.Features.Category.Services;
using StoreProject.Features.Product.DTOs;
using StoreProject.Features.Product.Services;

namespace StoreProject.Features.Category.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IProductManagementService _productManagementService;
        private readonly ICategoryManagementService _categoryManagementService;
        public CategoriesController(IProductManagementService productManagementService, ICategoryManagementService categoryManagementService)
        {
            _productManagementService = productManagementService;
            _categoryManagementService = categoryManagementService;
        }

        [Route("Categories/{slug}")]
        public IActionResult Details(string slug,
            string productTitle, string productSlug, decimal? minPrice, decimal? maxPrice, int pageId = 1)
        {

            var category = _categoryManagementService.GetCategoryBy(slug);
            if (category == null)
                return NotFound();

            var Parameters = new ProductFilterParamsDto()
            {
                PageId = pageId,
                Take = 9,
                CategoryId = category.Id,
                Title = productTitle,
                Slug = productSlug,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
            };

            var model = new DetailsModel()
            {
                Title = category.Title,
                Filter = _productManagementService.GetProductsByFilter(Parameters),
            };

            model.ProductsCount = model.Filter.EntityCount;

            return View(model);
        }
    }
}
