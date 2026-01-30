
using Microsoft.Extensions.Logging;

public class InMemoryOrderRepository : IOrderRepository
{
    private readonly List<Order> _db = new();
    private readonly ILogger<InMemoryOrderRepository> _logger;

    public InMemoryOrderRepository(ILogger<InMemoryOrderRepository> logger)
    {
        _logger = logger;
    }
    public Task<bool> ExistsExternalOrderAsync(int externalOrderId)
        => Task.FromResult(_db.Any(o => o.ExternalOrderId == externalOrderId));

    public Task<Order?> GetByIdAsync(int id)
        => Task.FromResult(_db.FirstOrDefault(o => o.ExternalOrderId == id));

    public Task<IEnumerable<Order>> ListByStatusAsync(OrderStatus status)
        => Task.FromResult(_db.Where(o => o.Status == status).AsEnumerable());

    public Task SaveAsync(Order order)
    {
        _logger.LogInformation("Salvando pedido {OrderID}", order.ExternalOrderId);
        _db.Add(order);
        return Task.CompletedTask;
    }
}