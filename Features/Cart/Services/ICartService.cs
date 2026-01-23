using StoreProject.Common;
using StoreProject.Entities;
using StoreProject.Features.Cart.DTOs;
using StoreProject.Features.Cart.Model;
using StoreProject.Features.Order.DTOs;

namespace StoreProject.Features.Cart.Services
{
    public interface ICartService
    {
        OperationResult AddToCart(string userId, int productId);
        OperationResult DeleteFromCart(string userId, int productId);
        CartDto GetCart(int cartId);
        CartDto GetCart(string userId);
        void ClearCart(string userId);
        int GetCartItemsCount(string userId);
        bool IsProductInCart(string userId, int productId);
		(OperationResult, int?) Checkout(string userId, CheckoutDto checkoutDto);
        OperationResult UpdateQuantity(string userId, int productId, int quantity);
        CartModel? GetCartDetails(string userId);
    }
}
