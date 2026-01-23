using System.ComponentModel.DataAnnotations;

namespace StoreProject.Features.ContactMessage.Models
{
    public class OtherContactMessagesModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public DateTime SentAt { get; set; }

        [Required]
        public bool IsRead { get; set; }
    }
}
