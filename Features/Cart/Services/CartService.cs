using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreProject.Common;
using StoreProject.Entities;
using StoreProject.Features.Cart.DTOs;
using StoreProject.Features.Cart.Mapper;
using StoreProject.Features.Cart.Model;
using StoreProject.Features.Order.DTOs;
using StoreProject.Features.Order.Services;
using StoreProject.Features.Product.Services;
using StoreProject.Infrastructure.Data;
using System.Collections;


namespace StoreProject.Features.Cart.Services
{
    public class CartService : ICartService
    {
        private readonly ILogger<CartService> _logger;
        private readonly StoreContext _context;
        private readonly IProductManagementService _productManagementService;
        private readonly IOrderService _orderService;
        public CartService(StoreContext context, IProductManagementService productManagementService, IOrderService orderService, ILogger<CartService> logger)
        {
            _context = context;
            _productManagementService = productManagementService;
            _orderService = orderService;
            _logger = logger;
        }

        private Entities.Cart GetCartByUserId(string userId)
        {
            var cart = _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefault(c => c.UserId == userId);
            return cart;
        }

        public OperationResult AddToCart(string userId, int productId)
        {
            var cart = GetCartByUserId(userId);
            if (cart == null)
            {
                cart = new Entities.Cart()
                {
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow,
                    CartItems = new List<Entities.CartItem>()
                };
                _context.Carts.Add(cart);
            }

            var cartItem = cart.CartItems.FirstOrDefault(i => i.ProductId == productId);
            var product = _productManagementService.GetProductBy(productId);
            if (product == null)
                return OperationResult.NotFound(["Product not found."]);
            if (cartItem != null)
            {
                if (cartItem.Quantity < product.Stock && cartItem.Quantity < 10)
                    cartItem.Quantity += 1;
                else
                {
                    return OperationResult.Error(["Maximum quantity reached."]);
                }
            }
            else
            {
                if (!_productManagementService.IsInStock(productId))
                    return OperationResult.Error(["Product is out of stock."]);

                cart.CartItems.Add(new Entities.CartItem()
                {
                    //CartId = cart.Id,
                    ProductId = productId,
                    Quantity = 1
                });
            }

            try
            {
                _context.SaveChanges();
                return OperationResult.Success();
            }
            catch (DbUpdateException ex) when (ex.InnerException?.Message.Contains("UQ_CartItem_CartId_ProductId") == true)
            {
                _logger.LogWarning(ex, $"Duplicate product add attempt. User {userId}, Product {productId}");

                return OperationResult.Error(["This item has already been added to your cart."]);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unexpected error in AddToCart. User {userId}, Product {productId}");
                return OperationResult.Error(["An unexpected error occurred while adding to cart."]);
            }
        }

        public (OperationResult, int?) Checkout(string userId, CheckoutDto checkoutDto)
        {
			using var transaction = _context.Database.BeginTransaction();
            try
            {
                var order = _orderService.CreateOrder(userId, checkoutDto);

                foreach (var item in order.OrderItems)
                {
                    var success = _productManagementService.ReserveQuantity(item.ProductId, item.Quantity);
                    if (!success)
                    {
                        _logger.LogWarning(
                            $"Checkout failed: Product {item.ProductId} does not have enough stock. User: {userId}"
                        );
                        transaction.Rollback();
                        return (OperationResult.Error([$"Product {item.Product.Title} is out of stock."]), order.Id);
                    }
                }

                ClearCart(userId);

                try
                {
                    _context.SaveChanges(acceptAllChangesOnSuccess: false);
                    transaction.Commit();
                    _context.ChangeTracker.AcceptAllChanges();

                    return (OperationResult.Success(), order.Id);
                }
                catch (DbUpdateException ex) when (ex.InnerException?.Message.Contains("UQ_CartItem_CartId_ProductId") == true)
                {
                    _logger.LogWarning(ex, $"Duplicate cart item detected during checkout. User {userId}");

                    transaction.Rollback();
                    return (OperationResult.Error(["An error occurred while placing the order: a duplicate item was detected in your cart."]), order.Id);
                }
            }                
            
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Checkout failed for user {userId}.");

                transaction.Rollback();
                return (OperationResult.Error(["An error occurred in checking out."]), null);
            }
        }

        public void ClearCart(string userId)
        {
            var cart = GetCartByUserId(userId);
            if (cart == null)
                return;

            _context.CartItems.RemoveRange(cart.CartItems);
        }

        public OperationResult DeleteFromCart(string userId, int productId)
        {
            var cart = GetCartByUserId(userId);
            if (cart == null)
                return OperationResult.NotFound(["Cart not found."]);

            var cartItem = cart.CartItems.FirstOrDefault(i => (i.ProductId == productId));
            if (cartItem == null)
                return OperationResult.NotFound(["The product is not in the cart."]);

            _context.CartItems.Remove(cartItem);

            _context.SaveChanges();
            return OperationResult.Success();
        }

        public CartDto GetCart(int cartId)
        {
            var cart = _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(c => c.Id == cartId);
            if (cart == null)
                return new CartDto();

            var cartDto = CartMapper.MapCartToCartDto(cart);
            return cartDto;
        }

        public CartDto GetCart(string userId)
        {
            var cart = _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(c => c.UserId == userId);
            if (cart == null)
                return new CartDto();

            var cartDto = CartMapper.MapCartToCartDto(cart);
            return cartDto;
        }

        public int GetCartItemsCount(string userId)
        {
            var cart = GetCartByUserId(userId);
            if (cart == null || cart.CartItems.Count == 0)
                return 0;
            return cart.CartItems.Sum(i => i.Quantity);
        }

        public int GetQuantity(string userId, int productId)
        {
            int quantity = 0;
            bool IsInCart = IsProductInCart(userId, productId);
            if (IsInCart)
            {
                var cart = GetCart(userId);
                quantity = cart.CartItems.FirstOrDefault(i => i.ProductId == productId).Quantity;
            }

            return quantity;
        }

        public bool IsProductInCart(string userId, int productId)
        {
            return _context.CartItems
                .Any(i => i.Cart.UserId == userId && i.ProductId == productId);
        }

        public OperationResult UpdateQuantity(string userId, int productId, int quantity)
        {
            if (quantity <= 0)
                return OperationResult.Error(["Quantity must be greater than zero."]);

            var stock = _productManagementService.GetProductStock(productId);
            if (quantity > stock)
                return OperationResult.Error([$"Only {stock} items available in stock."]);


            var cart = GetCartByUserId(userId);
            if (cart == null)
                return OperationResult.NotFound(["Cart not found."]);

            var product = cart.CartItems.FirstOrDefault(i => i.ProductId == productId);
            if(product == null)
                return OperationResult.NotFound(["Product not found."]);

            if(quantity > 10)
                return OperationResult.Error(["You can get at most 10 items of this product in one order."]);

            product.Quantity = quantity;
            _context.SaveChanges();
            return OperationResult.Success();
        }

        public CartModel? GetCartDetails(string userId)
        {
            var cart = GetCart(userId);
            if (cart == null || cart.CartItems.Count == 0)
                return null;

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
                    Quantity = i.Quantity
                };
            }).ToList();

            var cartDetails = new CartModel()
            {
                CartProducts = cartProducts,
                Total = cartProducts.Sum(cp => cp.Quantity * cp.UnitPrice)
            };

            return cartDetails;
        }
    }
}
