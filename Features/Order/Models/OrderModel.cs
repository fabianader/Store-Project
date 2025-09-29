using StoreProject.Features.Order.DTOs;

namespace StoreProject.Features.Order.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string ShippingAddress { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatusDto Status { get; set; }
        public List<OrderItemModel> OrderItems { get; set; }
    }
}
