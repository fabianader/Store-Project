using StoreProject.Common.Attributes;
using System.ComponentModel.DataAnnotations;

namespace StoreProject.Features.ContactMessage.Models
{
    public class ContactUsModel
    {
        [Required(ErrorMessage = "Enter your Name.")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Enter your Email.")]
        [EmailAddress]
        [EmailValidation]
        public string Email { get; set; }


        [Required(ErrorMessage = "Write the Subject.")]
        public string Subject { get; set; }


        [Required(ErrorMessage = "Write the Message.")]
        public string Message { get; set; }
    }
}
