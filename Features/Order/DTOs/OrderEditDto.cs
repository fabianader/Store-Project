namespace StoreProject.Features.Order.DTOs
{
    public class OrderEditDto
    {
        public int Id { get; set; }
        public OrderStatusDto Status { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; } 
        public string Address { get; set; }
    }
}
    