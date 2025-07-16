using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreProject.Entities
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        public string AuthorId { get; set; }
        public int CategoryId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Slug { get; set; }

        public string Body { get; set; }

        public DateTime CreatedAt { get; set; }


        [ForeignKey("AuthorId")]
        public ApplicationUser Author { get; set; }


        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
