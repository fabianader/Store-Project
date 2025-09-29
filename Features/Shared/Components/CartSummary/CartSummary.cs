using Microsoft.AspNetCore.Mvc;
using StoreProject.Features.Cart.Services;
using System.Security.Claims;

namespace StoreProject.Features.Shared.Components.CartSummary
{
    public class CartSummary : ViewComponent
    {
        private readonly ICartService _cartService;
        public CartSummary(ICartService cartService)
        {
            _cartService = cartService;
        }

        public IViewComponentResult Invoke()
        {
            string? userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int cartItemsCount = (userId != null) ? _cartService.GetCartItemsCount(userId) : 0;
            return View("CartSummary", cartItemsCount);
        }
    }
}
