namespace StoreProject.Features.Category.DTOs
{
    public class EditCategoryDto
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public bool IsDeleted { get; set; }
    }
}
