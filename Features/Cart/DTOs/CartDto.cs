using StoreProject.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreProject.Features.Cart.DTOs
{
    public class CartDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<CartItemDto> CartItems { get; set; }
    }
}
