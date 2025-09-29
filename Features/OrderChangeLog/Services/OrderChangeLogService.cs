using StoreProject.Features.Order.DTOs;

namespace StoreProject.Features.OrderChangeLog.Services
{
    public class OrderChangeLogService : IOrderChangeLogService
    {
        public List<Entities.OrderChangeLog> AddChangeLog(Entities.Order existingOrder, OrderEditDto updatedOrder, string adminUsername)
        {
            var logs = new List<Entities.OrderChangeLog>();
            void AddLog(string field, string oldValue, string newValue)
            {
                if(oldValue != newValue)
                {
                    logs.Add(new Entities.OrderChangeLog()
                    {
                        FieldChanged = field,
                        OldValue = oldValue,
                        NewValue = newValue,
                        ChangedAt = DateTime.UtcNow,
                        ChangedBy = adminUsername,
                        OrderId = existingOrder.Id
                    });
                }
            }

            AddLog(nameof(Entities.Order.FullName), existingOrder.FullName, updatedOrder.FullName);
            AddLog(nameof(Entities.Order.Email), existingOrder.Email, updatedOrder.Email);
            AddLog(nameof(Entities.Order.PhoneNumber), existingOrder.PhoneNumber, updatedOrder.PhoneNumber);
            AddLog(nameof(Entities.Order.ShippingAddress), existingOrder.ShippingAddress, updatedOrder.Address);
            AddLog(nameof(Entities.Order.Status), Enum.GetName(existingOrder.Status), Enum.GetName(updatedOrder.Status));

            return logs;
        }
    }
}
