using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreProject.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }


        [Required]
        public string Slug { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public Category Parent { get; set; }

        public ICollection<Category> Children { get; set; } = new List<Category>();
        public ICollection<Product> Products { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
