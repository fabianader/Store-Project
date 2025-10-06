using StoreProject.Features.User.DTOs;
using System.ComponentModel.DataAnnotations;

namespace StoreProject.Features.User.Model
{
    public class ResetPasswordModel
    {
        [Required]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Enter new password.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Enter the password again.")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "Password confirmation does not match.")]
        public string ConfirmPassword { get; set; }
    }
}
