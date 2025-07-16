namespace StoreProject.Features.User.DTOs
{
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePicture { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
