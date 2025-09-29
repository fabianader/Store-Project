using StoreProject.Common;

namespace StoreProject.Features.Order.DTOs
{
    public class OrderFilterDto : BasePagination
    {
        public List<AdminOrderDto> Orders { get; set; }
        public OrderFilterParamsDto OrderFilterParams { get; set; }
    }

    public class OrderFilterParamsDto
    {
        public int PageId { get; set; }
        public int Take { get; set; }


        public string UserName { get; set; }
        public string FullName { get; set; }
        public OrderStatusDto? Status { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
