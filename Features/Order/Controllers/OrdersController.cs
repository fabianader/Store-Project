using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            return View();
        }
    }
}
