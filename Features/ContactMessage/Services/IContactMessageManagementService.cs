using StoreProject.Common;
using StoreProject.Features.ContactMessage.DTOs;

namespace StoreProject.Features.ContactMessage.Services
{
    public interface IContactMessageManagementService
    {
        List<AdminContactMessageDto> GetAllContactMessages();
        List<AdminContactMessageDto> GetUserOtherContactMessages(string userId, int messageId);
        ContactMessageFilterDto GetContactMessagesByFilter(ContactMessageFilterParamsDto contactMessageFilterParamsDto);
        OperationResult MarkMessageAsRead(int id);
    }
}
