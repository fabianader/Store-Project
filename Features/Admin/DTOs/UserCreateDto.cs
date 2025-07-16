using StoreProject.Features.User.DTOs;
using System.ComponentModel.DataAnnotations;

namespace StoreProject.Features.Admin.DTOs
{
    public class UserCreateDto : UserRegisterDto
    {
        public List<string> UserRoles { get; set; }
    }
}
