using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StoreProject.Common;
using StoreProject.Entities;
using StoreProject.Features.Order.DTOs;
using StoreProject.Features.Order.Mapper;
using StoreProject.Features.OrderChangeLog.Services;
using StoreProject.Features.User.Services;
using StoreProject.Infrastructure.Data;
using System.Security.Claims;

namespace StoreProject.Features.Order.Services
{
    public class OrderService : IOrderService
    {
        private readonly StoreContext _context;
        private readonly IOrderChangeLogService _orderChangeLogService;
        private readonly IUserManagementService _userManagementService;
        public OrderService(StoreContext context, IOrderChangeLogService orderChangeLogService, IUserManagementService userManagementService)
        {
            _context = context;
            _orderChangeLogService = orderChangeLogService;
            _userManagementService = userManagementService;
        }

        public Entities.Order CreateOrder(string userId, CheckoutDto checkoutDto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            var cart = _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(c => c.UserId == userId);

            var order = new Entities.Order()
            {
                UserId = userId,
                FullName = checkoutDto.FullName,
                Email = checkoutDto.Email,
                PhoneNumber = checkoutDto.PhoneNumber,
                User = user,
                ShippingAddress = checkoutDto.Address,
                OrderDate = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                OrderItems = cart.CartItems
                    .Select(i => OrderMapper.MapCartItemToOrderItem(i)).ToList()
            };

            order.TotalPrice = order.OrderItems.Sum(i => i.Quantity * i.UnitPrice);

            _context.Orders.Add(order);
            return order;
        }

        public async Task<OperationResult> EditOrderAsync(OrderEditDto orderEditDto, string userId)
        {
            var adminUsername = await _userManagementService.GetUserNameAsync(userId);
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderEditDto.Id);
            if (order == null)
                return OperationResult.NotFound(["Order not found."]);
            
            var changeLogs = _orderChangeLogService.AddChangeLog(order, orderEditDto, adminUsername);

            OrderMapper.MapOrderEditDtoToOrder(order, orderEditDto);

            if (changeLogs.Count != 0)
                await _context.OrderChangeLogs.AddRangeAsync(changeLogs);

            await _context.SaveChangesAsync();

            return OperationResult.Success();
        }

        public OperationResult CancelOrder(int orderId)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
                return OperationResult.NotFound(["Order not found"]);

            if (order.Status != OrderStatus.Pending)
                return OperationResult.Error(["It is not possible to cancel the order."]);

            order.Status = OrderStatus.Cancelled;
            return OperationResult.Success();
        }

        public List<OrderDto> GetAllOrders()
        {
            var orders = _context.Orders
                .OrderByDescending(o => o.OrderDate)
                .Include(o => o.User)
                .Select(o => OrderMapper.MapOrderToOrderDto(o)).ToList();

            return orders;
        }

        public List<OrderDto> GetUserOrders(string userId)
        {
            var orders = _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                .OrderByDescending(o => o.OrderDate)
                .Select(OrderMapper.MapOrderToOrderDto).ToList();
            var c = orders;
            return orders;
        }

        public int GetUserOrdersCount(string userId)
        {
            var userOrders = _context.Orders.Select(o => o.UserId == userId);
            return userOrders.Count();
        }

        public OrderDto GetOrderBy(int orderId)
        {
            var order = _context.Orders
                .Include(o => o.OrderItems)
                .Include(o => o.User)
                .FirstOrDefault(o => o.Id == orderId);

            if (order == null)
                return null;

            var orderDto = OrderMapper.MapOrderToOrderDto(order);
            return orderDto;
        }

        public OrderFilterDto GetOrdersByFilter(OrderFilterParamsDto orderFilterParamsDto)
        {
            var result = _context.Orders
                .OrderByDescending(o => o.OrderDate)
                .Include(o => o.User).AsQueryable();

            if(!orderFilterParamsDto.UserName.IsNullOrEmpty())
                result = result.Where(o => o.User.UserName.Contains(orderFilterParamsDto.UserName));

            if(!orderFilterParamsDto.FullName.IsNullOrEmpty())
                result = result.Where(o => o.FullName.Contains(orderFilterParamsDto.FullName));

            if (orderFilterParamsDto.FromDate.HasValue)
                result = result.Where(o => o.OrderDate >= orderFilterParamsDto.FromDate);
            if (orderFilterParamsDto.ToDate.HasValue)
                result = result.Where(o => o.OrderDate <= orderFilterParamsDto.ToDate);

            if(orderFilterParamsDto.Status.HasValue)
                result = result.Where(o => o.Status == OrderMapper
                    .MapOrderStatusDtoToOrderStatus(orderFilterParamsDto.Status.Value));

            var skip = (orderFilterParamsDto.PageId - 1) * orderFilterParamsDto.Take;
            var orderFilter = new OrderFilterDto()
            {
                Orders = result.Skip(skip).Take(orderFilterParamsDto.Take)
                    .Select(o => OrderMapper.MapOrderToAdminOrderDto(o)).ToList(),
                OrderFilterParams = orderFilterParamsDto
            };

            orderFilter.GeneratePaging(result, orderFilterParamsDto.Take, orderFilterParamsDto.PageId);
            return orderFilter;
        }

        public OrderDto GetPendingOrderBy(int orderId)
        {
            var order = _context.Orders
                .FirstOrDefault(o => o.Status == OrderStatus.Pending && o.Id == orderId);
            if (order == null)
                return null;
            var orderDto = OrderMapper.MapOrderToOrderDto(order);
            return orderDto;
        }
    }
}
