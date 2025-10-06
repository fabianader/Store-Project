using StoreProject.Features.Order.DTOs;

namespace StoreProject.Features.Order.Models
{
    public class UserPanelOrderDetailsModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string ShippingAddress { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatusDto Status { get; set; }
        public List<UserPanelOrderItemModel> OrderItems { get; set; }
    }
}
