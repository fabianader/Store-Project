using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreProject.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }


        public DateTime OrderDate { get; set; }


        [Required]
        public string ShippingAddress { get; set; }

        [Required]
        [Precision(18, 4)]
        public decimal TotalPrice { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public ICollection<OrderItem> OrderItems { get; set; }
    }

    public enum OrderStatus
    {
        Pending = 0,     // تازه ثبت شده
        Paid = 1,        // پرداخت شده
        Shipped = 2,     // ارسال شده
        Delivered = 3,   // تحویل داده شده
        Cancelled = 4    // لغو شده
    }
}
