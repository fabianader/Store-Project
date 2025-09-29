using System.ComponentModel.DataAnnotations;

namespace StoreProject.Features.Cart.Model
{
    public class CartModel
    {
        public List<CartProductModel> CartProducts { get; set; }
        public string? CallBackUrl { get; set; }

        [DataType(DataType.Currency)]
        public decimal Total { get; set; }
    }
    public class CartProductModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Slug { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}
