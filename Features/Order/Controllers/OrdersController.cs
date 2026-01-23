using Microsoft.AspNetCore.Mvc;
using StoreProject.Common;
using StoreProject.Features.Order.Services;

namespace StoreProject.Features.Order.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Payment(int orderId)
        {
            var order = _orderService.GetOrderBy(orderId);
            if (order == null)
                return NotFound();

            return View(orderId);
        }

           
        public IActionResult PaymentResult(int orderId, bool isPaymentSuccessful)
        {
			var order = _orderService.GetOrderBy(orderId);
			if (order == null)
				return NotFound();

            OperationResult result;
            if (isPaymentSuccessful)
            {
                result = _orderService.ConfirmOrder(orderId);
            }
            else
            {
                result = _orderService.CancelOrder(orderId);
            }

            if (result.Status != OperationResultStatus.Success)
                return BadRequest();

			return RedirectToAction("Index", "Home");
        }
    }
}
