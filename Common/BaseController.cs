using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace StoreProject.Common
{
    public class BaseController : Controller
    {
        protected IActionResult RedirectAndShowAlert(OperationResult result, IActionResult redirectPath)
        {
            var model = JsonConvert.SerializeObject(result);
            HttpContext.Response.Cookies.Append("SystemAlert", model);
            if (result.Status != OperationResultStatus.Success)
                return View();

            return redirectPath;
        }
        protected IActionResult PartialViewAndShowErrorAlert(PartialViewResult partialViewName, List<string>? ErrorMessages=null)
        {
            ErrorAlert(ErrorMessages);
            return partialViewName;
        }

        protected IActionResult RedirectToUrlAndShowErrorAlert(OperationResult result, IActionResult redirectPath)
        {
            var model = JsonConvert.SerializeObject(result);
            HttpContext.Response.Cookies.Append("SystemAlert", model);
            if (result.Status != OperationResultStatus.Success)
                return redirectPath;

            return View();
        }

        protected void SuccessAlert()
        {
            var model = JsonConvert.SerializeObject(OperationResult.Success());
            HttpContext.Response.Cookies.Append("SystemAlert", model);
        }
        protected void SuccessAlert(List<string> message)
        {
            var model = JsonConvert.SerializeObject(OperationResult.Success(message));
            HttpContext.Response.Cookies.Append("SystemAlert", model);
        }
        protected void ErrorAlert()
        {
            var model = JsonConvert.SerializeObject(OperationResult.Error());
            HttpContext.Response.Cookies.Append("SystemAlert", model);
        }
        protected void ErrorAlert(List<string> message)
        {
            var model = JsonConvert.SerializeObject(OperationResult.Error(message));
            HttpContext.Response.Cookies.Append("SystemAlert", model);
        }
    }
}
