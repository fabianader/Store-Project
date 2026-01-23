namespace StoreProject.Features.ContactMessage.DTOs
{
    public class UserPanelContactMessageDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }
    }
}
