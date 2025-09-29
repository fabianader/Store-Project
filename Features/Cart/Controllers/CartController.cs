using Microsoft.AspNetCore.Mvc;
using StoreProject.Common;
using StoreProject.Features.Cart.Model;
using StoreProject.Features.Cart.Services;
using StoreProject.Features.Order.DTOs;
using StoreProject.Features.Product.Services;
using StoreProject.Features.Shared.Components.CartSummary;
using System.Security.Claims;

namespace StoreProject.Features.Cart.Controllers
{
    public class CartController : BaseController
    {
        private readonly ICartService _cartService;
        private readonly IProductManagementService _productManagementService;

        public CartController(ICartService cartService, IProductManagementService productManagementService)
        {
            _cartService = cartService;
            _productManagementService = productManagementService;
        }

        private string? GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public IActionResult Index(string receivedUrl = "")
        {
            ViewBag.CartIsEmpty = false;
            var userId = GetUserId();

            if (userId == null)
                return Unauthorized();

            var cart = _cartService.GetCart(userId);
            if (cart.CartItems == null || cart.CartItems.Count == 0)
            {
                ViewBag.CartIsEmpty = true;
                return View("Cart");
            }

            var cartProducts = cart.CartItems.Select(i =>
            {
                var p = _productManagementService.GetProductBy(i.ProductId);
                return new CartProductModel()
                {
                    Id = i.ProductId,
                    Title = p.Title,
                    Slug = p.Slug,
                    ImageUrl = p.ImageUrl,
                    UnitPrice = p.Price,
                    Quantity = i.Quantity,
                };
            }).ToList();
            string continueShoppingUrl = CommonService.GetContinueShoppingUrl(receivedUrl);
            var model = new CartModel
            {
                CallBackUrl = continueShoppingUrl,
                CartProducts = cartProducts,
                Total = cartProducts.Sum(cp => cp.Quantity * cp.UnitPrice)
            };

            return View("Cart", model);
        }


        [HttpPost]
        public IActionResult AddToCart(int productId, string callBackUrl)
        {
            callBackUrl ??= "/Home/Index";
            string? userId = GetUserId();
            if (userId == null)
                return RedirectAndShowAlert(OperationResult.Error(["User not found."]), Redirect(callBackUrl));

            var result = _cartService.AddToCart(userId, productId);
            if (result.Status != OperationResultStatus.Success)
                return RedirectAndShowAlert(result, Redirect(callBackUrl));

            return PartialView("_AddToCart", callBackUrl);
        }

        public IActionResult DeleteFromCart(int productId, string callBackUrl)
        {
            string? userId = GetUserId();
            if (userId == null)
                return Unauthorized();

            var result = _cartService.DeleteFromCart(userId, productId);
            if (result.Status != OperationResultStatus.Success)
                return BadRequest(result.Message);

            return Redirect(callBackUrl);
        }


        public IActionResult Checkout()
        {
            var userId = GetUserId();
            if (userId == null)
                return Unauthorized();

            var cartDetails = _cartService.GetCartDetails(userId);
            if(cartDetails == null)
                return NotFound();

            var model = new CheckoutModel()
            {
                CartDetails = cartDetails
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout(CheckoutModel model)
        {
            var userId = GetUserId();
            if (userId == null)
                return Unauthorized();

            var cartDetails = _cartService.GetCartDetails(userId);
            if (cartDetails == null)
                return NotFound();

            model.CartDetails = cartDetails;
            if (!ModelState.IsValid)
                return View(model);

            var checkoutDto = new CheckoutDto()
            {
                FullName = model.FullName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address
            };

            var result = _cartService.Checkout(userId, checkoutDto);
            if (result.Status != OperationResultStatus.Success)
            {
                ModelState.AddModelError(string.Empty, "An error occurred.");
                return View(model);
            }

            return RedirectToAction("Payment", "Orders", new {userId});
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            string? userId = GetUserId();
            if (userId == null)
                return Unauthorized();

            var result = _cartService.UpdateQuantity(userId, productId, quantity);
            if (result.Status != OperationResultStatus.Success)
                return BadRequest();

            var cart = _cartService.GetCart(userId);
            var product = _productManagementService.GetProductBy(productId);

            decimal productTotal = product.Price * quantity;
            decimal total = cart.CartItems.Sum(i =>
                _productManagementService.GetProductBy(i.ProductId).Price * i.Quantity
            );
            int cartQuantity = _cartService.GetCartItemsCount(userId);

            return Json(new { productTotal, total, cartQuantity });
        }

    }
}
