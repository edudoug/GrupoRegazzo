public class GetOrderUseCase
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderUseCase(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public Task<Order?> ExecuteAsync(int id)
        => _orderRepository.GetByIdAsync(id);
}