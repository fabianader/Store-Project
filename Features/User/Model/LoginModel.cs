using System.ComponentModel.DataAnnotations;

namespace StoreProject.Features.User.Model
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Enter the Username")]
        public string UserName { get; set; }

        
        [Required(ErrorMessage = "Enter the Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
