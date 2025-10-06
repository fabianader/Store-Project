﻿using StoreProject.Common;
using StoreProject.Features.Order.DTOs;

namespace StoreProject.Features.Order.Services
{
    public interface IOrderService
    {
        OrderDto GetPendingOrderBy(int orderId);
        List<OrderDto> GetAllOrders();
        List<OrderDto> GetUserOrders(string userId);
        int GetUserOrdersCount(string userId);
        OrderFilterDto GetOrdersByFilter(OrderFilterParamsDto orderFilterParamsDto);
        Task<OperationResult> EditOrderAsync(OrderEditDto orderEditDto, string userId);
        OperationResult CancelOrder(int orderId);
        OrderDto GetOrderBy(int orderId);
        Entities.Order CreateOrder(string userId, CheckoutDto checkoutDto);
    }
}
