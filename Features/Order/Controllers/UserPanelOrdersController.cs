using Microsoft.AspNetCore.Mvc;
using StoreProject.Common;
using StoreProject.Features.Order.Mapper;
using StoreProject.Features.Order.Models;
using StoreProject.Features.Order.Services;
using StoreProject.Features.Product.Services;
using System.Security.Claims;

namespace StoreProject.Features.Order.Controllers
{
    [Route("UserPanel/Orders/{action=index}")]
    public class UserPanelOrdersController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IProductManagementService _productManagementService;
        public UserPanelOrdersController(IOrderService orderService, IProductManagementService productServiceManagement)
        {
            _orderService = orderService;
            _productManagementService = productServiceManagement;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return NotFound();

            ViewBag.HasNoOrders = false;

            var orders = _orderService.GetUserOrders(userId);
            if (orders.Count == 0)
            {
                ViewBag.HasNoOrders = true;
                return View();
            }

            var model = orders.Select(OrderMapper.MapOrderDtoToUserPanelOrderModel).ToList();
            return View(model);
        }

        public IActionResult ViewDetails(int orderId)
        {
            var order = _orderService.GetOrderBy(orderId);
            if (order == null)
                return View();

            var model = OrderMapper.MapOrderDtoToUserPanelOrderDetailsModel(order);
            foreach (var item in model.OrderItems)
            {
                item.ProductTitle = _productManagementService.GetProductTitle(item.ProductId);
            }

            return View(model);
        }

        public IActionResult ViewCancelOrderModal(int orderId, string callBackUrl)
        {
            var model = new UserPanelCancelOrderModel()
            {
                OrderId = orderId,
                CallBackUrl = callBackUrl
            };
            return PartialView("_CancelOrder", model);
        }

        public IActionResult CancelOrder(int orderId)
        {
            var order = _orderService.GetOrderBy(orderId);
            if (order == null)
            {
                ErrorAlert(["Order not found."]);
                return RedirectToAction("ViewDetails", new { orderId });
            }

            var result = _orderService.CancelOrder(orderId);
            if (result.Status != OperationResultStatus.Success)
            {
                ErrorAlert(result.Message);
                return RedirectToAction("ViewDetails", new { orderId });
            }

            return RedirectAndShowAlert(result,
                RedirectToAction("ViewDetails", new { orderId }));
        }
    }
}
