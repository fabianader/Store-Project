using StoreProject.Entities;
using StoreProject.Features.Cart.DTOs;

namespace StoreProject.Features.Cart.Mapper
{
    public class CartMapper
    {
        public static CartItemDto MapCartItemToCartItemDto(CartItem cartItem)
        {
            return new CartItemDto()
            {
                Id = cartItem.Id,
                CartId = cartItem.CartId,
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity,
            };
        }

        public static CartDto MapCartToCartDto(Entities.Cart cart)
        {
            return new CartDto()
            {
                Id = cart.Id,
                UserId = cart.UserId,
                CreatedAt = cart.CreatedAt,
                CartItems = cart.CartItems.Select(i => MapCartItemToCartItemDto(i)).ToList()
            };
        }
    }
}
