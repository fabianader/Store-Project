using System.ComponentModel.DataAnnotations;

namespace StoreProject.Entities
{
    public class OrderChangeLog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public string ChangedBy { get; set; }

        [Required]
        public DateTime ChangedAt { get; set; }

        [Required]
        public string FieldChanged { get; set; }

        [Required]
        public string OldValue { get; set; }

        [Required]
        public string NewValue { get; set; }
    }
}
