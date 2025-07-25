﻿using StoreProject.Common.Attributes;
using System.ComponentModel.DataAnnotations;

namespace StoreProject.Features.Admin.Model
{
    public class UserEditModel
    {
        [Required]
        public string Id { get; set; }


        [Display(Name = "Username")]
        [Required(ErrorMessage = "Enter the {0}")]
        public string UserName { get; set; }


        [Display(Name = "FullName")]
        [Required(ErrorMessage = "Enter the {0}")]
        public string FullName { get; set; }


        [Display(Name = "Email")]
        [Required(ErrorMessage = "Enter the {0}")]
        [EmailAddress]
        [EmailValidation]
        public string Email { get; set; }


        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Enter the {0}")]
        [Phone]
        public string PhoneNumber { get; set; }


        public string ProfilePictureName { get; set; }
        

        [Display(Name = "Profile Picture")]
        public IFormFile? ProfilePictureFile { get; set; }


        [Display(Name = "User Roles")]
        [Required(ErrorMessage = "Enter the {0}")]
        public List<string> UserRoles { get; set; }


        [Display(Name = "Status")]
        [Required(ErrorMessage = "Enter the {0}")]
        public bool IsDeleted { get; set; }
    }
}
