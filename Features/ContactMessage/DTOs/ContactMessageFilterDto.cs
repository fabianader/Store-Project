using StoreProject.Common;

namespace StoreProject.Features.ContactMessage.DTOs
{
    public class ContactMessageFilterDto : BasePagination
    {
        public List<AdminContactMessageDto> ContactMessages { get; set; }
        public ContactMessageFilterParamsDto ContactMessageFilterParams { get; set; }
    }

    public class ContactMessageFilterParamsDto
    {
        public int PageId { get; set; }
        public int Take { get; set; }

        public string UserName { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public bool IsRead { get; set; }
        public DateTime? SentAt { get; set; }
    }
}
