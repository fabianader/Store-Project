using System.ComponentModel.DataAnnotations;

namespace StoreProject.Features.User.Model
{
    public class SixDigitEmailCodeModel
    {
        [Required]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Enter code from email")]
        public string Code { get; set; }

        [Required]
        public string CalledByAction { get; set; }
    }
}
