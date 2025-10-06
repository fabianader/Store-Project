namespace StoreProject.Features.Order.Models
{
    public class UserPanelOrderItemModel
    {
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
