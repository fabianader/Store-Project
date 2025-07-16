using Microsoft.AspNetCore.Mvc;

namespace StoreProject.Features.Admin.Controllers
{
    [Route("Admin/{action=index}")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
