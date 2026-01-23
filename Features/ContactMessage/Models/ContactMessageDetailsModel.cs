using System.ComponentModel.DataAnnotations;

namespace StoreProject.Features.ContactMessage.Models
{
    public class ContactMessageDetailsModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public bool IsRead { get; set; }

        [Required]
        public DateTime SentAt { get; set; }
    }
}
