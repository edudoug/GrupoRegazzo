public interface IOrderRepository
{
    Task<bool> ExistsExternalOrderAsync(int externalOrderId);
    Task SaveAsync(Order order);
    Task<Order?> GetByIdAsync(int id);
    Task<IEnumerable<Order>> ListByStatusAsync(OrderStatus status);
}