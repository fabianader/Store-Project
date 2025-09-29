using StoreProject.Features.Order.DTOs;

namespace StoreProject.Features.OrderChangeLog.Services
{
    public interface IOrderChangeLogService
    {
        List<Entities.OrderChangeLog> AddChangeLog(Entities.Order existingOrder, OrderEditDto updatedOrder, string adminUsername);
    }
}
