using StoreProject.Features.User.DTOs;
using System.ComponentModel.DataAnnotations;

namespace StoreProject.Features.User.Model
{
    public class ResetPasswordModel
    {
        [Required]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Enter New Password.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Enter the Password again.")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "Password confirmation does not match.")]
        public string RePassword { get; set; }
    }
}
