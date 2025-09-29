using StoreProject.Common.Attributes;
using System.ComponentModel.DataAnnotations;

namespace StoreProject.Features.Cart.Model
{
    public class CheckoutModel
    {
        [Required(ErrorMessage = "Enter the full name.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Enter the phone number.")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Enter the Email.")]
        [EmailAddress]
        [EmailValidation]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter the address.")]
        public string Address { get; set; }


        public CartModel? CartDetails { get; set; }
    }
}
