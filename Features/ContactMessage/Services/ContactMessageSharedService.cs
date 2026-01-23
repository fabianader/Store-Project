using StoreProject.Features.ContactMessage.DTOs;
using StoreProject.Features.ContactMessage.Mapper;
using StoreProject.Infrastructure.Data;

namespace StoreProject.Features.ContactMessage.Services
{
    public class ContactMessageSharedService : IContactMessageSharedService
    {
        private readonly StoreContext _context;
        public ContactMessageSharedService(StoreContext context)
        {
            _context = context;
        }

        public ContactMessageDto GetContactMessageBy(int id)
        {
            var contactMessage = _context.ContactMessages
                .FirstOrDefault(cm => cm.Id == id);
            if (contactMessage == null)
                return null;

            var contactMessageDto = ContactMessageMapper.MapContactMessageToContactMessageDto(contactMessage);
            return contactMessageDto;
        }
    }
}
