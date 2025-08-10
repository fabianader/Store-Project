using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace StoreProject.Features.Product.Model
{
    public class ProductBaseModel
    {
        [Display(Name = "Category")]
        [Required(ErrorMessage = "Choose the {0}")]
        public int CategoryId { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Enter the {0}")]
        public string Title { get; set; }

        [Display(Name = "Slug")]
        [Required(ErrorMessage = "Enter the {0}")]
        public string Slug { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Enter the {0}")]
        public string Description { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Enter the {0}")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:F4}")]
        // [Remote(action: "IsNonNegative", controller: "ProductsController", AdditionalFields = "__RequestVerificationToken", HttpMethod = "Post")]
        public decimal Price { get; set; }

        [Display(Name = "Stock")]
        [Required(ErrorMessage = "Enter the {0}")]
        // [Remote(action: "IsNonNegative", controller: "ProductsController", AdditionalFields = "__RequestVerificationToken", HttpMethod = "Post")]
        public int Stock { get; set; }
    }
}
