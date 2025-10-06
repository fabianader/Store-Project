namespace StoreProject.Features.User.Model
{
    public class UserPanelProfileModel
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePicture { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}
