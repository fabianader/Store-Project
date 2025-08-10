namespace StoreProject.Features.Shared.Model
{
	public class CategoryTreeModel
	{
		public string Title { get; set; }
		public string Slug { get; set; }
		public List<ChildCategory> ChildCategories { get; set; }
	}

	public class ChildCategory
	{
        public string Title { get; set; }
        public string Slug { get; set; }
    }
}
