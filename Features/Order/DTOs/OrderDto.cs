using Microsoft.EntityFrameworkCore;
using StoreProject.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StoreProject.Features.Order.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string ShippingAddress { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatusDto Status { get; set; } = OrderStatusDto.Pending;
        public List<OrderItemDto> OrderItems { get; set; }
    }

    public enum OrderStatusDto
    {
        Pending = 0,
        Paid = 1,
        Shipped = 2,
        Delivered = 3,
        Cancelled = 4
    }
}
