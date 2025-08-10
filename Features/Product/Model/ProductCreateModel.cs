using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace StoreProject.Features.Product.Model
{
    public class ProductCreateModel : ProductBaseModel
    {
        [Display(Name = "Product Image")]
        [Required(ErrorMessage = "Upload the {0}")]
        public IFormFile ProductImage { get; set; }
    }
}
