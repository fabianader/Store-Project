using System.ComponentModel.DataAnnotations;

namespace StoreProject.Features.User.DTOs
{
    public class UserCreateDto : UserRegisterDto
    {
        public List<string> UserRoles { get; set; }
    }
}
