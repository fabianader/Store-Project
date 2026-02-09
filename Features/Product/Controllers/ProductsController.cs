using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using StoreProject.Common;
using StoreProject.Common.Services;
using StoreProject.Entities;
using StoreProject.Features.Cart.Services;
using StoreProject.Features.Product.DTOs;
using StoreProject.Features.Product.Mapper;
using StoreProject.Features.Product.Model;
using StoreProject.Features.Product.Services;
using System.Security.Claims;

namespace StoreProject.Features.Product.Controllers
{
    [Route("Products/{action}")]
    public class ProductsController : BaseController
    {
        private readonly IProductManagementService _productManagementService;
        private readonly ICartService _cartService;
        public ProductsController(IProductManagementService productManagementService, ICartService cartService)
        {
            _productManagementService = productManagementService;
            _cartService = cartService;
        }

        [Route("/Products/{slug}")]
        public IActionResult Details(string slug)
        {
            var product = _productManagementService.GetProductBy(slug);
            if (product == null)
                return RedirectAndShowMessage("info", "Product not found!");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = ProductMapper.MapProductDtoToDetailsModel(product);
            model.ProductQuantity = _cartService.GetQuantity(userId, product.Id);
            
            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SearchPost(string q)
        {
            return RedirectToActionPermanent("Search", new { q });
        }

        public IActionResult Search(string q)
        {
            ViewBag.q = q;
            var model = _productManagementService.SearchProductsByTitle(q);
            return View("Search", model);
        }
    }
}
