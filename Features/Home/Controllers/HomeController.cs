using Microsoft.AspNetCore.Mvc;

namespace StoreProject.Features.Home.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
