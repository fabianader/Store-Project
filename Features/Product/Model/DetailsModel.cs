namespace StoreProject.Features.Product.Model
{
    public class DetailsModel : ProductBaseModel
    {
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public string CategoryTitle { get; set; }
        public string CategorySlug { get; set; }
        public string ImageUrl { get; set; }
    }
}
