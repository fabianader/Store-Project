using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreProject.Entities
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }


        [ForeignKey("OrderId")]
        public Order Order { get; set; }


        [ForeignKey("ProductId")]
        public Product Product { get; set; }


        [Required]
        public int Quantity { get; set; }

        [Required]
        [Precision(18, 4)]
        public decimal UnitPrice { get; set; }
    }
}
