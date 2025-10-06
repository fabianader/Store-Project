using StoreProject.Entities;
using StoreProject.Features.Order.DTOs;
using StoreProject.Features.Order.Models;

namespace StoreProject.Features.Order.Mapper
{
    public class OrderMapper
    {
        public static OrderItem MapCartItemToOrderItem(CartItem cartItem)
        {
            return new OrderItem()
            {
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity,
                UnitPrice = cartItem.Product.Price,
            };
        }

        public static OrderStatus MapOrderStatusDtoToOrderStatus(OrderStatusDto orderStatusDto)
        {
            return orderStatusDto switch
            {
                OrderStatusDto.Pending => OrderStatus.Pending,
                OrderStatusDto.Paid => OrderStatus.Paid,
                OrderStatusDto.Shipped => OrderStatus.Shipped,
                OrderStatusDto.Delivered => OrderStatus.Delivered,
                OrderStatusDto.Cancelled => OrderStatus.Cancelled,

                _ => throw new ArgumentOutOfRangeException(nameof(orderStatusDto), orderStatusDto, null)
            };
        }

        public static OrderStatusDto MapOrderStatusToOrderStatusDto(OrderStatus orderStatus)
        {
            return orderStatus switch
            {
                OrderStatus.Pending => OrderStatusDto.Pending,
                OrderStatus.Paid => OrderStatusDto.Paid,
                OrderStatus.Shipped => OrderStatusDto.Shipped,
                OrderStatus.Delivered => OrderStatusDto.Delivered,
                OrderStatus.Cancelled => OrderStatusDto.Cancelled,

                _ => throw new ArgumentOutOfRangeException(nameof(orderStatus), orderStatus, null)
            };
        }

        public static OrderItem MapOrderItemCreateDtoToOrderItem(OrderItemCreateDto orderItemCreateDto)
        {
            return new OrderItem()
            {
                OrderId = orderItemCreateDto.OrderId,
                ProductId = orderItemCreateDto.ProductId,
                Quantity = orderItemCreateDto.Quantity,
                UnitPrice = orderItemCreateDto.UnitPrice,
            };
        }

        public static OrderItemDto MapOrderItemToOrderItemDto(Entities.OrderItem orderItem)
        {
            return new OrderItemDto()
            {
                Id = orderItem.Id,
                OrderId = orderItem.OrderId,
                ProductId= orderItem.ProductId,
                Quantity= orderItem.Quantity,
                UnitPrice = orderItem.UnitPrice
            };
        }

        public static OrderDto MapOrderToOrderDto(Entities.Order order)
        {
            return new OrderDto()
            {
                Id = order.Id,
                UserId = order.UserId,
                FullName = order.FullName,
                Email = order.Email,
                PhoneNumber = order.PhoneNumber,
                OrderDate = order.OrderDate,
                ShippingAddress = order.ShippingAddress,
                TotalPrice = order.TotalPrice,
                Status = MapOrderStatusToOrderStatusDto(order.Status),
                OrderItems = order.OrderItems
                    .Select(i => MapOrderItemToOrderItemDto(i)).ToList()
            };
        }

        //public static OrderModel MapOrderFilterDtoToOrderModel(OrderFilterDto orderFilterDto)
        //{
        //    return new OrderModel()
        //    {
        //        Id = orderFilterDto.
        //    };
        //}

        //public static UserPanelOrderDetailsModel MapOrderDtoToOrderModel(OrderDto orderDto)
        //{
        //    return new UserPanelOrderDetailsModel()
        //    {
        //        Id = orderDto.Id,
        //        UserId = orderDto.UserId,
        //        FullName = orderDto.FullName,
        //        Email = orderDto.Email,
        //        PhoneNumber = orderDto.PhoneNumber,
        //        OrderDate = orderDto.OrderDate,
        //        ShippingAddress = orderDto.ShippingAddress,
        //        Status = orderDto.Status,
        //        TotalPrice = orderDto.TotalPrice,
        //        OrderItems = orderDto.OrderItems
        //            .Select(i => MapOrderItemDtoToOrderItemModel(i)).ToList()
        //    };
        //}

        public static OrderItemModel MapOrderItemDtoToOrderItemModel(OrderItemDto orderItemDto)
        {
            return new OrderItemModel()
            {
                Id = orderItemDto.Id,
                OrderId = orderItemDto.OrderId,
                ProductId = orderItemDto.ProductId,
                Quantity = orderItemDto.Quantity,
                UnitPrice = orderItemDto.UnitPrice
            };
        }

        public static AdminOrderDto MapOrderToAdminOrderDto(Entities.Order order)
        {
            return new AdminOrderDto()
            {
                Id = order.Id,
                UserId = order.UserId,
                UserName = order.User.UserName,
                FullName = order.FullName,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                Status = MapOrderStatusToOrderStatusDto(order.Status),
            };
        }

        public static Entities.Order MapOrderEditDtoToOrder(Entities.Order order, OrderEditDto orderEditDto)
        {
            order.FullName = orderEditDto.FullName;
            order.PhoneNumber = orderEditDto.PhoneNumber;
            order.Email = orderEditDto.Email;
            order.ShippingAddress = orderEditDto.Address;
            order.Status = MapOrderStatusDtoToOrderStatus(orderEditDto.Status);

            return order;
        }

        public static OrderDetailsModel MapOrderDtoToOrderEditModel(OrderDto order)
        {
            return new OrderDetailsModel()
            {
                FullName = order.FullName,
                Email = order.Email,
                PhoneNumber = order.PhoneNumber,
                Address = order.ShippingAddress,
                Status = order.Status,
                UserId = order.UserId,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice
            };
        }

        public static UserPanelOrderDetailsModel MapOrderDtoToUserPanelOrderDetailsModel(OrderDto order)
        {
            return new UserPanelOrderDetailsModel()
            {
                Id = order.Id,
                FullName = order.FullName,
                Email = order.Email,
                PhoneNumber = order.PhoneNumber,
                OrderDate = order.OrderDate,
                ShippingAddress = order.ShippingAddress,
                TotalPrice = order.TotalPrice,
                Status = order.Status,
                OrderItems = order.OrderItems
                    .Select(MapOrderItemDtoToUserPanelOrderItemModel).ToList()
            };
        }

        public static UserPanelOrderItemModel MapOrderItemDtoToUserPanelOrderItemModel(OrderItemDto orderItemDto)
        {
            return new UserPanelOrderItemModel()
            {
                ProductId = orderItemDto.ProductId,
                Quantity = orderItemDto.Quantity,
                UnitPrice = orderItemDto.UnitPrice,
            };
        }

        public static UserPanelOrderModel MapOrderDtoToUserPanelOrderModel(OrderDto order)
        {
            return new UserPanelOrderModel()
            {
                Id = order.Id,
                FullName = order.FullName,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                Status = order.Status,
                OrderItems = order.OrderItems
                    .Select(MapOrderItemDtoToUserPanelOrderItemModel).ToList()
            };
        }
    }
}
