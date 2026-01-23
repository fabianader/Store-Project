using StoreProject.Features.ContactMessage.DTOs;
using StoreProject.Features.ContactMessage.Models;

namespace StoreProject.Features.ContactMessage.Mapper
{
    public class ContactMessageMapper
    {
        public static ContactMessageDto MapContactMessageToContactMessageDto(Entities.ContactMessage contactMessage)
        {
            return new ContactMessageDto()
            {
                Id = contactMessage.Id,
                UserId = contactMessage.UserId,
                Name = contactMessage.Name,
                Email = contactMessage.Email,
                Subject = contactMessage.Subject,
                Message = contactMessage.Message,
                SentAt = contactMessage.SentAt,
                IsRead = contactMessage.IsRead
            };
        }

        public static AdminContactMessageDto MapContactMessageToAdminContactMessageDto(Entities.ContactMessage contactMessage)
        {
            return new AdminContactMessageDto()
            {
                Id = contactMessage.Id,
                UserId = contactMessage.UserId,
                UserName = contactMessage.User.UserName,
                Name = contactMessage.Name,
                Subject = contactMessage.Subject,
                SentAt = contactMessage.SentAt,
                IsRead = contactMessage.IsRead
            };
        }

        public static Entities.ContactMessage MapSendContactMessageDtoToContactMessage(SendContactMessageDto sendContactMessageDto)
        {
            return new Entities.ContactMessage()
            {
                UserId = sendContactMessageDto.UserId,
                Email = sendContactMessageDto.Email,
                Name = sendContactMessageDto.Name,
                Subject = sendContactMessageDto.Subject,
                Message = sendContactMessageDto.Message,
                SentAt = DateTime.UtcNow,
                IsRead = false                
            };
        }

        public static UserPanelContactMessageDto MapContactMessageToUserPanelContactMessageDto(Entities.ContactMessage contactMessage)
        {
            return new UserPanelContactMessageDto()
            {
                Id = contactMessage.Id,
                Name = contactMessage.Name,
                Email = contactMessage.Email,
                Subject = contactMessage.Subject,
                SentAt = contactMessage.SentAt,
                IsRead = contactMessage.IsRead
            };
        }

        public static ContactMessageDetailsModel MapContactMessageDtoToContactMessageDetailsModel(ContactMessageDto contactMessageDto)
        {
            return new ContactMessageDetailsModel()
            {
                Id = contactMessageDto.Id,
                UserId = contactMessageDto.UserId,
                Name = contactMessageDto.Name,
                Email = contactMessageDto.Email,
                Subject = contactMessageDto.Subject,
                Message = contactMessageDto.Message,
                IsRead = contactMessageDto.IsRead,
                SentAt = contactMessageDto.SentAt                
            };
        }

        public static OtherContactMessagesModel MapAdminContactMessageDtoToOtherContactMessagesModel(AdminContactMessageDto adminContactMessageDto)
        {
            return new OtherContactMessagesModel()
            {
                Id = adminContactMessageDto.Id,
                Name = adminContactMessageDto.Name,
                Subject = adminContactMessageDto.Subject,
                SentAt = adminContactMessageDto.SentAt,
                IsRead = adminContactMessageDto.IsRead
            };
        }

        public static UserPanelContactMessageModel MapUserPanelContactMessageDtoToUserPanelContactMessageModel(UserPanelContactMessageDto userPanelContactMessageDto)
        {
            return new UserPanelContactMessageModel()
            {
                Id = userPanelContactMessageDto.Id,
                Name = userPanelContactMessageDto.Name,
                Email = userPanelContactMessageDto.Email,
                Subject = userPanelContactMessageDto.Subject,
                IsRead = userPanelContactMessageDto.IsRead,
                SentAt = userPanelContactMessageDto.SentAt
            };
        }

        public static UserPanelContactMessageDetailsModel MapContactMessageDtoToUserPanelContactMessageDetailsModel(ContactMessageDto contactMessageDto)
        {
            return new UserPanelContactMessageDetailsModel()
            {
                Id = contactMessageDto.Id,
                Name = contactMessageDto.Name,
                Email = contactMessageDto.Email,
                Subject = contactMessageDto.Subject,
                Message = contactMessageDto.Message,
                IsRead = contactMessageDto.IsRead,
                SentAt = contactMessageDto.SentAt
            };
        }
    }
}
