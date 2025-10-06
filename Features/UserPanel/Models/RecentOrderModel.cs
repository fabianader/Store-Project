using StoreProject.Features.Order.DTOs;

namespace StoreProject.Features.UserPanel.Models
{
    public class RecentOrderModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatusDto Status { get; set; }
        public List<RecentOrderItemModel> OrderItems { get; set; }
    }
}
