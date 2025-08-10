namespace StoreProject.Features.Category.Model
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsDeleted { get; set; }
        public int? ParentId { get; set; }
    }
}
