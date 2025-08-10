using StoreProject.Entities;

namespace StoreProject.Features.Category.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsDeleted { get; set; }
        public int? ParentId { get; set; }
    }
}
