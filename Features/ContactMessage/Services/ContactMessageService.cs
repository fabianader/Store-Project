using Microsoft.AspNetCore.Components;
using StoreProject.Common;
using StoreProject.Features.ContactMessage.DTOs;
using StoreProject.Features.ContactMessage.Mapper;
using StoreProject.Infrastructure.Data;

namespace StoreProject.Features.ContactMessage.Services
{
    public class ContactMessageService : IContactMessageService
    {
        private readonly StoreContext _context;
        public ContactMessageService(StoreContext context)
        {
            _context = context;
        }

        public OperationResult SendContactMessage(SendContactMessageDto sendContactMessageDto)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Id == sendContactMessageDto.UserId);
            if (user == null)
                return OperationResult.NotFound(["User not found."]);

            var contactMessage = ContactMessageMapper.MapSendContactMessageDtoToContactMessage(sendContactMessageDto);
            _context.ContactMessages.Add(contactMessage);
            _context.SaveChanges();

            return OperationResult.Success();
        }

        public List<UserPanelContactMessageDto> UserPanelGetContactMessages(string userId)
        {
            var userPanelContactMessagesDto = _context.ContactMessages
                .Where(cm => cm.UserId == userId)
                .OrderByDescending(cm => cm.SentAt)
                .Select(ContactMessageMapper.MapContactMessageToUserPanelContactMessageDto)
                .ToList();

            return userPanelContactMessagesDto;
        }
    }
}
