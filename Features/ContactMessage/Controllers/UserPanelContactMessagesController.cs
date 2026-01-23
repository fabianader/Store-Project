using Microsoft.AspNetCore.Mvc;
using StoreProject.Features.ContactMessage.Mapper;
using StoreProject.Features.ContactMessage.Services;
using System.Security.Claims;

namespace StoreProject.Features.ContactMessage.Controllers
{
    [Route("UserPanel/Messages/{action=index}")]
    public class UserPanelContactMessagesController : Controller
    {
        private readonly IContactMessageService _contactMessageService;
        private readonly IContactMessageSharedService _contactMessageSharedService;
        public UserPanelContactMessagesController(IContactMessageService contactMessageService, IContactMessageSharedService contactMessageSharedService)
        {
            _contactMessageService = contactMessageService;
            _contactMessageSharedService = contactMessageSharedService;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return NotFound();

            var contactMessageDtos = _contactMessageService.UserPanelGetContactMessages(userId);
            var model = contactMessageDtos.Select(ContactMessageMapper.MapUserPanelContactMessageDtoToUserPanelContactMessageModel).ToList();
            return View(model);
        }

        public IActionResult ViewDetails(int messageId)
        {
            var contactMessage = _contactMessageSharedService.GetContactMessageBy(messageId);
            if (contactMessage == null)
                return View();

            var model = ContactMessageMapper.MapContactMessageDtoToUserPanelContactMessageDetailsModel(contactMessage);
            return View(model);
        }
    }
}
