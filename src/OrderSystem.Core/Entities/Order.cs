public class Order
{
    public int ExternalOrderId { get; }
    public int CustumerId { get; }
     public decimal Tax { get; private set; }
    public OrderStatus Status { get; private set; }
    public IReadOnlyCollection<OrderItem> Items { get; }

    public Order(int externalOrderId, int custumerId, List<OrderItem> items)
    {
        ExternalOrderId = externalOrderId;
        CustumerId = custumerId;
        Items = items;
        Status = OrderStatus.Created;
    }

    public decimal TotalValue()
        => Items.Sum(i => i.Price * i.Quantity);

    public void ApplyTax(decimal tax)
        => Tax = tax;

}