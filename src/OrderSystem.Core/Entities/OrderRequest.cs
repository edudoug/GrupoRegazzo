public class OrderRequest
{
    public int pedidoId { get; set; }
    public int clienetId { get; set; }
    public List<OrderItem> items{ get; set; }
}