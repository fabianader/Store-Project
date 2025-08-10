using System.ComponentModel.DataAnnotations;

namespace StoreProject.Features.Product.Model
{
    public class ProductEditModel : ProductBaseModel
    {
        [Required]
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        [Display(Name = "Product Image")]
        public IFormFile? ProductImage { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Enter the {0}")]
        public bool IsDeleted { get; set; }
    }
}
