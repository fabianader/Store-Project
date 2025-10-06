namespace StoreProject.Features.UserPanel.Models
{
    public class RecentOrderItemModel
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
