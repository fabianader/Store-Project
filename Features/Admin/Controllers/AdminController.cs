using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StoreProject.Features.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("Admin/{action=index}")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
