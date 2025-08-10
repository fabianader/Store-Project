using System.ComponentModel.DataAnnotations;

namespace StoreProject.Features.Category.Model
{
    public class CategoryCreateModel
    {
        [Display(Name = "Title")]
        [Required(ErrorMessage = "Enter the {0}")]
        public string Title { get; set; }


        [Display(Name = "Slug")]
        [Required(ErrorMessage = "Enter the {0}")]
        public string Slug { get; set; }


        [Display(Name = "Parent Category")]
        public int? ParentId { get; set; }
    }
}
