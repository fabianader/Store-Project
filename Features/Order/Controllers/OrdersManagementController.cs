using Microsoft.AspNetCore.Mvc;
using StoreProject.Common;
using StoreProject.Features.Order.DTOs;
using StoreProject.Features.Order.Mapper;
using StoreProject.Features.Order.Models;
using StoreProject.Features.Order.Services;
using StoreProject.Features.Product.Services;
using StoreProject.Features.User.Services;
using System.Security.Claims;

namespace StoreProject.Features.Order.Controllers
{
    [Route("Admin/OrdersManagement/{action=index}")]
    public class OrdersManagementController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IUserManagementService _userManagementService;
        private readonly IProductManagementService _prodManagementService;
        public OrdersManagementController(IOrderService orderService, IUserManagementService userManagementService, IProductManagementService prodManagementService)
        {
            _orderService = orderService;
            _userManagementService = userManagementService;
            _prodManagementService = prodManagementService;
        }

        public IActionResult Index(DateTime? FromDate, DateTime? ToDate, int? status,
                                   string username = "", string fullname = "", int pageId = 1)
        {
            var parameters = new OrderFilterParamsDto()
            {
                PageId = pageId,
                UserName = username,
                Status = (OrderStatusDto?)status,
                FullName = fullname,
                FromDate = FromDate,
                ToDate = ToDate,
                Take = 10
            };
            var model = _orderService.GetOrdersByFilter(parameters);
            return View(model);
        }

        public IActionResult OrderDetails(int id, string username)
        {
            var order = _orderService.GetOrderBy(id);
            if (order == null)
            {
                ErrorAlert(["Order not found."]);
                return RedirectToAction("Index");
            }

            var model = OrderMapper.MapOrderDtoToOrderEditModel(order);
            model.Id = id;
            model.UserName = username;
            model.OrderItems = order.OrderItems
                .Select(oi => new OrderItemModel()
                {
                    Id = oi.Id,
                    OrderId = oi.OrderId,
                    ProductId = oi.ProductId,
                    ProductTitle = _prodManagementService.GetProductTitle(oi.ProductId),
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList();

            ViewBag.OrderStatusIndex = (int)order.Status;

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderDetails(OrderDetailsModel model)
        {
            if(!ModelState.IsValid)
            {
                ErrorAlert();
                return View(model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(userId == null)
            {
                ErrorAlert(["User not found."]);
                return View(model);
            }

            var result = await _orderService.EditOrderAsync(new OrderEditDto()
            {
                Id = model.Id,
                FullName = model.FullName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                Status = model.Status
            }, userId);

            if(result.Status != OperationResultStatus.Success)
            {
                ErrorAlert(result.Message);
                return View(model);
            }

            return RedirectAndShowAlert(result, RedirectToAction("Index"));
        }
    }
}
