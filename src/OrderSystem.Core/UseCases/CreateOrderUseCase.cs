public class CreateOrderUseCase
{
    private readonly IOrderRepository _orderRepository;
    private readonly IFeatureFlagPort _featureFlagPort;
    private readonly CurrentTaxService _currentTaxService;
    private readonly TaxReformService _taxReformService;

    public CreateOrderUseCase(
        IOrderRepository orderRepository,
        IFeatureFlagPort featureFlagPort,
        CurrentTaxService currentTaxService,
        TaxReformService taxReformService
    )
    {
        _orderRepository = orderRepository;
        _featureFlagPort = featureFlagPort;
        _currentTaxService = currentTaxService;
        _taxReformService = taxReformService;
    }
    public async Task<CreateOrderResult> ExecuteAsync(OrderRequest OrderRequest)
    {
        if(await _orderRepository.ExistsExternalOrderAsync(OrderRequest.pedidoId))
            throw new Exception("Pedido Duplicado");

        var items = OrderRequest.items
            .Select(i => new OrderItem(i.ProductId, i.Quantity, i.Price))
            .ToList();

        var order = new Order(OrderRequest.pedidoId, OrderRequest.clienetId, items);

        var tax = _featureFlagPort.IsTaxReformEnabled()
            ? _taxReformService.Calculate(order.TotalValue())
            : _currentTaxService.Calculate(order.TotalValue());

        order.ApplyTax(tax);
        await _orderRepository.SaveAsync(order);
        return new CreateOrderResult(order.ExternalOrderId, order.Status);
    }
}