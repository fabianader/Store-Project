namespace StoreProject.Features.ContactMessage.DTOs
{
    public class AdminContactMessageDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public bool IsRead { get; set; }
        public DateTime SentAt { get; set; }
    }
}
