using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/orders")]
public class OrderController: ControllerBase
{
    private readonly CreateOrderUseCase _createOrderUseCase;
    private readonly GetOrderUseCase _getOrderUseCase;
    private readonly ListOrderUseCase _listOrderUseCase;
    private readonly ILogger<OrderController> _logger;

    public OrderController(
        CreateOrderUseCase createOrderUseCase,
        GetOrderUseCase getOrderUseCase,
        ListOrderUseCase listOrderUseCase,
        ILogger<OrderController> logger
    )
    {
        _createOrderUseCase = createOrderUseCase;
        _getOrderUseCase = getOrderUseCase;
        _listOrderUseCase = listOrderUseCase;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Create(OrderRequest request)
    {
        _logger.LogInformation("Ordem recebida {request.pedidoId}", request.pedidoId);

        var order = await _createOrderUseCase.ExecuteAsync(request);

        return Created("", new { order });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        _logger.LogInformation("Resgatando ordem {id}", id);

        var order = await _getOrderUseCase.ExecuteAsync(id);

        return Created("", new { order });
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        _logger.LogInformation("Resgatando ordens");

        var ordens = await _listOrderUseCase.ExecuteAsync(0);

        return Created("", new { ordens });
    }
}