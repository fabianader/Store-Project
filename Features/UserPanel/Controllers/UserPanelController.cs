using Microsoft.AspNetCore.Mvc;
using StoreProject.Features.Order.Services;
using StoreProject.Features.Product.Services;
using StoreProject.Features.User.Services;
using StoreProject.Features.UserPanel.Mapper;
using StoreProject.Features.UserPanel.Models;
using System.Security.Claims;

namespace StoreProject.Features.UserPanel.Controllers
{
    public class UserPanelController : Controller
    {
        private readonly IUserManagementService _userManagementService;
        private readonly IProductManagementService _productManagementService;
        private readonly IOrderService _orderService;
        public UserPanelController(IUserManagementService userManagementService, IOrderService orderService, IProductManagementService productManagementService)
        {
            _userManagementService = userManagementService;
            _orderService = orderService;
            _productManagementService = productManagementService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return NotFound();

            ViewBag.HasNoOrders = false;
            
            var userOrders = _orderService.GetUserOrders(userId);
            if (userOrders.Count == 0)
            {
                ViewBag.HasNoOrders = true;
                return View();
            }

            var model = new DashboardModel()
            {
                UserOrdersCount = userOrders.Count,
                UserRecentOrders = userOrders.Take(3).Select(UserPanelMapper.MapOrderDtoToRecentOrderModel).ToList(),
            };

            foreach(var recentOrder in model.UserRecentOrders)
            {
                recentOrder.OrderItems
                    .Select(oi => oi.ProductTitle = _productManagementService.GetProductTitle(oi.ProductId));
            }
            

            return View(model);
        }
    }
}
