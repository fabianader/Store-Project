namespace StoreProject.Features.User.DTOs
{
    public class UserPanelChangePasswordDto
    {
        public string UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
