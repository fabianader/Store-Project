using Microsoft.AspNetCore.Mvc;
using StoreProject.Common;
using StoreProject.Features.ContactMessage.DTOs;
using StoreProject.Features.ContactMessage.Models;
using StoreProject.Features.ContactMessage.Services;
using System.Security.Claims;

namespace StoreProject.Features.ContactMessage.Controllers
{
    public class ContactUsController : BaseController
    {
        private readonly IContactMessageService _contactMessageService;
        public ContactUsController(IContactMessageService contactMessageService)
        {
            _contactMessageService = contactMessageService;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]  
        [ValidateAntiForgeryToken]
        public IActionResult Index(ContactUsModel model)
        {
            if (!ModelState.IsValid)
            {
                ErrorAlert();
                return View(model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(userId == null)
                return NotFound();

            var result = _contactMessageService.SendContactMessage(new SendContactMessageDto()
            {
                UserId = userId,
                Name = model.Name,
                Email = model.Email,
                Subject = model.Subject,
                Message = model.Message,
            });

            return RedirectAndShowAlert(result, RedirectToAction("Index"));
        }
    }
}
