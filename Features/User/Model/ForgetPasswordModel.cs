using StoreProject.Common.Attributes;
using System.ComponentModel.DataAnnotations;

namespace StoreProject.Features.User.Model
{
    public class ForgetPasswordModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [EmailValidation]
        public string Email { get; set; }
    }
}
