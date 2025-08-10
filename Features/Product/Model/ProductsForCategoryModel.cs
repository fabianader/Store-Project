using System.ComponentModel.DataAnnotations;

namespace StoreProject.Features.Product.Model
{
    public class ProductsForCategoryModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Slug { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; }
    }
}
