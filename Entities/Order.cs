using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreProject.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }


        public DateTime OrderDate { get; set; }


        [Required]
        public string ShippingAddress { get; set; }

        [Required]
        [Precision(18, 4)]
        public decimal TotalPrice { get; set; }

        public enum Status
        {   
            Pending,
            Paid,
            Shipped
        }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
