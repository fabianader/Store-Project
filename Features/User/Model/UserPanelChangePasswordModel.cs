using System.ComponentModel.DataAnnotations;

namespace StoreProject.Features.User.Model
{
    public class UserPanelChangePasswordModel
    {
        [Display(Name = "Current password")]
        [Required(ErrorMessage = "Enter current password.")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }


        [Display(Name = "New password")]
        [Required(ErrorMessage = "Enter new password.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }


        [Display(Name = "Confirm New Password")]
        [Required(ErrorMessage = "Enter the password again.")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "Password confirmation does not match.")]
        public string ConfirmPassword { get; set; }
    }
}
