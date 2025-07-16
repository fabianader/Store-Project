using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreProject.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FullName {  get; set; }

        public DateTime RegisterDate { get; set; }

        public string ProfilePicture { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public Cart Cart { get; set; }


        public ICollection<Order> Orders { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<ContactMessage> ContactMessages { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
        public virtual ICollection<IdentityUserToken<string>> Tokens { get; set; }
        public virtual ICollection<IdentityUserRole<string>> UserRoles { get; set; }
    }
}
