using StoreProject.Common.Attributes;
using System.ComponentModel.DataAnnotations;

namespace StoreProject.Features.User.Model
{
    public class RegisterModel
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Enter the {0}")]
        public string UserName { get; set; }


        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Enter the {0}")]
        public string FullName { get; set; }


        [Display(Name = "Email")]
        [EmailAddress]
        [EmailValidation]
        [Required(ErrorMessage = "Enter the {0}")]
        public string Email { get; set; }


        [Display(Name = "Phone Number")]
        [Phone]
        [Required(ErrorMessage = "Enter the {0}")]
        public string PhoneNumber { get; set; }


        [Display(Name = "Password")]
        [Required(ErrorMessage = "Enter the {0}")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name = "Repeat Password")]
        [Required(ErrorMessage = "Enter the {0}")]
        [Compare(nameof(Password), ErrorMessage = "Password confirmation does not match.")]
        [DataType(DataType.Password)]
        public string RePassword { get; set; }
    }
}
