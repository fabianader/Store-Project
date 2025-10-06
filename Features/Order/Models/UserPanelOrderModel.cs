using StoreProject.Features.Order.DTOs;
using StoreProject.Features.UserPanel.Models;

namespace StoreProject.Features.Order.Models
{
    public class UserPanelOrderModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatusDto Status { get; set; }
        public List<UserPanelOrderItemModel> OrderItems { get; set; }
    }
}
