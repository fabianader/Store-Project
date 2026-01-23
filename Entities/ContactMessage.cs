using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreProject.Entities
{
    public class ContactMessage
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public bool IsRead { get; set; }

        public DateTime SentAt { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }
    }
}
