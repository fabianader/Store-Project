using Microsoft.AspNetCore.Mvc;

namespace StoreProject.Features.Shared.Controllers
{
    public class ErrorHandlerController : Controller
    {
        [Route("/ErrorHandler/{code}")]
        public IActionResult Index(int code)
        {
            switch (code)
            {
                case 404:
                    return View("NotFound");
                case 500:
                    return View("ServerError");

                default:
                    return View("NotFound");
            }
        }
    }
}
