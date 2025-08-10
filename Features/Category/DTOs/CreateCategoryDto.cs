namespace StoreProject.Features.Category.DTOs
{
    public class CreateCategoryDto
    {
        public int? ParentId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
    }
}
