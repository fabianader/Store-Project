using Microsoft.AspNetCore.Mvc;
using StoreProject.Features.Product.Services;

namespace StoreProject.Features.Shared.Components.BestSellingProducts
{
    public class BestSellingProducts : ViewComponent
    {
        private readonly IProductManagementService _productManagementService;
        public BestSellingProducts(IProductManagementService productManagementService)
        {
            _productManagementService = productManagementService;
        }

        public IViewComponentResult Invoke(int count)
        {
            var bestSellingProducts = _productManagementService.GetBestSellingProducts(count);
            return View("BestSellingProducts", bestSellingProducts);
        }
    }
}
