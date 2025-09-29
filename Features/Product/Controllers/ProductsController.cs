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
    public class ProductsController : Controller
    {
        private readonly IProductManagementService _productManagementService;
        private readonly ICartService _cartService;
        public ProductsController(IProductManagementService productManagementService, ICartService cartService)
        {
            _productManagementService = productManagementService;
            _cartService = cartService;
        }

        [Route("Products/{slug}")]
        public IActionResult Details(string slug)
        {
            var product = _productManagementService.GetProductBy(slug);
            if (product == null)
                return NotFound();

            var model = ProductMapper.MapProductDtoToDetailsModel(product);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // make this a service method...
            bool IsInCart = _cartService.IsProductInCart(userId, product.Id);
            int quantity;
            if (IsInCart)
            {
                var cart = _cartService.GetCart(userId);
                quantity = cart.CartItems
                    .FirstOrDefault(i => i.ProductId == product.Id).Quantity;
            }
            else
            {
                quantity = 0;
            }
            // -----------------------------
            model.ProductQuantity = quantity;
            return View(model);
        }
    }
}
