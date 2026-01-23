using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StoreProject.Common;
using StoreProject.Features.ContactMessage.DTOs;
using StoreProject.Features.ContactMessage.Mapper;
using StoreProject.Infrastructure.Data;

namespace StoreProject.Features.ContactMessage.Services
{
    public class ContactMessageManagementService : IContactMessageManagementService
    {
        private readonly StoreContext _context;
        public ContactMessageManagementService(StoreContext context)
        {
            _context = context;
        }

        public List<AdminContactMessageDto> GetAllContactMessages()
        {
            var AdminContactMessages = _context.ContactMessages
                .Include(cm => cm.User)
                .OrderByDescending(cm => cm.SentAt)
                .Select(ContactMessageMapper.MapContactMessageToAdminContactMessageDto)
                .ToList();

            return AdminContactMessages;
        }

        public List<AdminContactMessageDto> GetUserOtherContactMessages(string userId, int messageId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                return null;

            var userOtherContactMessages = _context.ContactMessages
                .Where(cm => cm.UserId == userId & cm.Id != messageId)
                .Include(cm => cm.User)
                .OrderByDescending(cm => cm.SentAt)
                .Select(ContactMessageMapper.MapContactMessageToAdminContactMessageDto).ToList();
            
            return userOtherContactMessages;
        }

        public ContactMessageFilterDto GetContactMessagesByFilter(ContactMessageFilterParamsDto contactMessageFilterParamsDto)
        {
            var result = _context.ContactMessages
                .OrderByDescending(cm => cm.SentAt)
                .Include(cm => cm.User).AsQueryable();

            if(!contactMessageFilterParamsDto.UserName.IsNullOrEmpty())
                result = result
                    .Where(cm => cm.User.UserName.Contains(contactMessageFilterParamsDto.UserName));

            if(!contactMessageFilterParamsDto.Name.IsNullOrEmpty())
                result = result
                    .Where(cm => cm.Name.Contains(contactMessageFilterParamsDto.Name));
            
            if(!contactMessageFilterParamsDto.Subject.IsNullOrEmpty())
                result = result
                    .Where(cm => cm.Subject.Contains(contactMessageFilterParamsDto.Subject));
            
            if(contactMessageFilterParamsDto.IsRead)
                result = result.Where(cm => cm.IsRead);
            else if(!contactMessageFilterParamsDto.IsRead)
                result = result.Where(cm => !cm.IsRead);

            if (contactMessageFilterParamsDto.SentAt != null)
                result = result.Where(cm => cm.SentAt == contactMessageFilterParamsDto.SentAt);

            var skip = (contactMessageFilterParamsDto.PageId - 1) * contactMessageFilterParamsDto.Take;

            var contactMessageFilter = new ContactMessageFilterDto()
            {
                ContactMessages = result.Skip(skip).Take(contactMessageFilterParamsDto.Take)
                    .Select(ContactMessageMapper.MapContactMessageToAdminContactMessageDto).ToList(),
                ContactMessageFilterParams = contactMessageFilterParamsDto
            };
            contactMessageFilter.GeneratePaging(result, contactMessageFilterParamsDto.Take, contactMessageFilterParamsDto.PageId);
            return contactMessageFilter;
        }

        public OperationResult MarkMessageAsRead(int id)
        {
            var contactMessage = _context.ContactMessages.FirstOrDefault(cm => cm.Id == id);
            if (contactMessage == null)
                return OperationResult.NotFound(["Contact Message not found."]);

            contactMessage.IsRead = true;
            _context.SaveChanges();
            return OperationResult.Success();
        }
    }
}
