using StoreProject.Features.Order.DTOs;
using StoreProject.Features.UserPanel.Models;

namespace StoreProject.Features.UserPanel.Mapper
{
    public class UserPanelMapper
    {
        public static RecentOrderModel MapOrderDtoToRecentOrderModel(OrderDto order)
        {
            return new RecentOrderModel()
            {
                Id = order.Id,
                FullName = order.FullName,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                Status = order.Status,
                OrderItems = order.OrderItems
                    .Select(MapOrderItemDtoToRecentOrderItemModel).ToList()
            };
        }

        public static RecentOrderItemModel MapOrderItemDtoToRecentOrderItemModel(OrderItemDto orderItem)
        {
            return new RecentOrderItemModel()
            {
                ProductId = orderItem.ProductId,
                OrderId = orderItem.OrderId,
                Quantity = orderItem.Quantity,
                UnitPrice = orderItem.UnitPrice
            };
        }
    }
}
