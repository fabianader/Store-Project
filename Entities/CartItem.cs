using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreProject.Entities
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }


        [ForeignKey("CartId")]
        public Cart Cart { get; set; }


        [ForeignKey("ProductId")]
        public Product Product { get; set; }


        [Required]
        public int Quantity { get; set; }
    }
}
