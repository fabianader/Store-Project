using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace StoreProject.Features.User.Model
{
    public class ResetPasswordEmailCodeModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Code { get; set; }
    }
}
