namespace StoreProject.Features.Order.DTOs
{
    public class AdminOrderDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatusDto Status { get; set; }
    }
}
