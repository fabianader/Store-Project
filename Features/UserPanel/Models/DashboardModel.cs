using StoreProject.Features.Order.DTOs;

namespace StoreProject.Features.UserPanel.Models
{
    public class DashboardModel
    {
        public int UserOrdersCount { get; set; }
        public List<RecentOrderModel> UserRecentOrders { get; set; }
    }
}
