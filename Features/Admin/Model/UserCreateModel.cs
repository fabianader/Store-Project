﻿using StoreProject.Features.User.Model;
using System.ComponentModel.DataAnnotations;

namespace StoreProject.Features.Admin.Model
{
    public class UserCreateModel : RegisterModel
    {
        [Required(ErrorMessage = "Enter the {0}")]
        public List<string> UserRoles { get; set; }
    }
}
