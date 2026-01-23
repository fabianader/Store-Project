using StoreProject.Common;
using StoreProject.Features.ContactMessage.DTOs;

namespace StoreProject.Features.ContactMessage.Services
{
    public interface IContactMessageService
    {
        OperationResult SendContactMessage(SendContactMessageDto sendContactMessageDto);
        
        List<UserPanelContactMessageDto> UserPanelGetContactMessages(string userId);
    }
}
