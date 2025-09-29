using StoreProject.Common.Attributes;
using StoreProject.Features.Order.DTOs;
using System.ComponentModel.DataAnnotations;

namespace StoreProject.Features.Order.Models
{
    public class OrderDetailsModel
    {
        [Required]
        public int Id { get; set; }


        [Display(Name = "Status")]
        [Required(ErrorMessage = "Enter the {0}")]
        public OrderStatusDto Status { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }


        [Display(Name = "Email")]
        [EmailAddress]
        [EmailValidation]
        public string Email { get; set; }


        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; }


        [Display(Name = "Address")]
        public string Address { get; set; }


        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItemModel> OrderItems { get; set; }
    }
}
