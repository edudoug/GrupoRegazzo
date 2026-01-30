public class ListOrderUseCase
{
    private readonly IOrderRepository _orderRepository;

    public ListOrderUseCase(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public Task<IEnumerable<Order>> ExecuteAsync(OrderStatus status)
        => _orderRepository.ListByStatusAsync(status);
}