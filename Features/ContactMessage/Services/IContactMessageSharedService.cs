using StoreProject.Features.ContactMessage.DTOs;

namespace StoreProject.Features.ContactMessage.Services
{
    public interface IContactMessageSharedService
    {
        ContactMessageDto GetContactMessageBy(int id);
    }
}
