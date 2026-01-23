namespace StoreProject.Features.ContactMessage.DTOs
{
    public class SendContactMessageDto
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }

    }
}
