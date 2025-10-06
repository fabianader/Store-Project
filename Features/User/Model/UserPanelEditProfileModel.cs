using StoreProject.Common.Attributes;
using System.ComponentModel.DataAnnotations;

namespace StoreProject.Features.User.Model
{
	public class UserPanelEditProfileModel
	{
		[Display(Name = "Username")]
		[Required(ErrorMessage = "Enter the {0}")]
		public string UserName { get; set; }


        [Display(Name = "Full name")]
        [Required(ErrorMessage = "Enter the {0}")]
		public string FullName { get; set; }


        [Display(Name = "Email")]
        [Required(ErrorMessage = "Enter the {0}")]
		[EmailAddress]
		[EmailValidation]
		public string Email { get; set; }


        [Display(Name = "Phone number")]
        [Required(ErrorMessage = "Enter the {0}")]
		[Phone]
		public string PhoneNumber { get; set; }

		
        [Display(Name = "Profile Picture")]
        public IFormFile? ProfilePictureFile { get; set; }

        public string ProfilePictureName { get; set; }
    }
}
