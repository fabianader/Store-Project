using Microsoft.AspNetCore.Mvc;
using StoreProject.Features.Product.Services;

namespace StoreProject.Features.Shared.Components.MostFavoritedProducts
{
    public class MostFavoritedProducts : ViewComponent
    {
        private readonly IProductManagementService _productManagementService;
        public MostFavoritedProducts(IProductManagementService productManagementService)
        {
            _productManagementService = productManagementService;
        }

        public IViewComponentResult Invoke(int count)
        {
            var mostFavoritedProducts = _productManagementService.GetMostFavoritedProducts(count);
            return View("MostFavoritedProducts", mostFavoritedProducts);
        }
    }
}
