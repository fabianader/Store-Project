using Microsoft.AspNetCore.Mvc;
using StoreProject.Common;
using StoreProject.Features.ContactMessage.DTOs;
using StoreProject.Features.ContactMessage.Mapper;
using StoreProject.Features.ContactMessage.Models;
using StoreProject.Features.ContactMessage.Services;
using StoreProject.Features.User.Services;

namespace StoreProject.Features.ContactMessage.Controllers
{
    [Route("Admin/ContactMessagesManagement/{action=index}")]
    public class ContactMessagesManagementController : BaseController
    {
        private readonly IContactMessageManagementService _contactMessageManagementService;
        private readonly IContactMessageSharedService _contactMessageSharedService;
        private readonly IUserManagementService _userManagementService;
        public ContactMessagesManagementController(IContactMessageManagementService contactMessageManagementService, IContactMessageSharedService contactMessageSharedService, IUserManagementService userManagementService)
        {
            _contactMessageManagementService = contactMessageManagementService;
            _contactMessageSharedService = contactMessageSharedService;
            _userManagementService = userManagementService;
        }

        public IActionResult Index(string? username, string? name,
                    string? subject, bool isRead, DateTime? sentAt, int pageId = 1)
        {
            ViewBag.ShowSeenMessages = isRead;
            var parameters = new ContactMessageFilterParamsDto()
            {
                PageId = pageId,
                Take = 5,
                UserName = username,
                Name = name,
                Subject = subject,
                IsRead = isRead,
                SentAt = sentAt
            };
            var model = _contactMessageManagementService.GetContactMessagesByFilter(parameters);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string queryValue)
        {
            string url = $"{Request.Path}";

            string[] queryStringKeyValues = Request.QueryString.Value.Replace("?", string.Empty).Split('&');
            if (Request.QueryString.Value.Contains("name"))
            {
                url += "?";
                for (int i = 0; i < queryStringKeyValues.Length; i++)
                {
                    if (queryStringKeyValues[i].Contains("name"))
                    {
                        queryStringKeyValues[i] = $"name={queryValue}";
                    }

                    url += (i != queryStringKeyValues.Length - 1) ? $"{queryStringKeyValues[i]}&" : queryStringKeyValues[i];
                }
            }
            else
            {
                url += Request.QueryString.Add("name", queryValue);
            }

            return Redirect(url);
        }

        public async Task<IActionResult> ContactMessageDetails(int id)
        {
            var contactMessage = _contactMessageSharedService.GetContactMessageBy(id);
            if (contactMessage == null)
            {
                ErrorAlert(["Message not found."]);
                return RedirectToAction("Index");
            }

            var model = ContactMessageMapper.MapContactMessageDtoToContactMessageDetailsModel(contactMessage);
            model.UserName = await _userManagementService.GetUserNameAsync(model.UserId);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MarkContactMessageAsRead(int id)
        {
            var result = _contactMessageManagementService.MarkMessageAsRead(id);
            if(result.Status != OperationResultStatus.Success)
            {
                return Json(new { success = false, message = result.Message });
            }

            return Json(new { success = true, message = "Marked as read." });
        }

        public IActionResult ViewOtherContactMessages(string userId, int messageId)
        {
            var otherContactMessages = _contactMessageManagementService.GetUserOtherContactMessages(userId, messageId);
            var model = otherContactMessages.Select(ContactMessageMapper.MapAdminContactMessageDtoToOtherContactMessagesModel).ToList();
            return View(model);
        }
    }
}
