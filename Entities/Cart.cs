using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreProject.Entities
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }


        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }


        public DateTime CreatedAt { get; set; }

        public ICollection<CartItem> CartItems { get; set; }
    }
}
