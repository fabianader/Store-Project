namespace StoreProject.Features.User.DTOs
{
    public class UserEditDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? ProfilePictureName { get; set; }
        public IFormFile? ProfilePicture { get; set; }
        public List<string> UserRoles { get; set; }
        public bool IsDeleted { get; set; }
    }
}
