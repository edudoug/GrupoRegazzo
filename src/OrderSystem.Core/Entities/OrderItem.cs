public class OrderItem
{
    public int ProductId { get; }
    public int Quantity { get; }
    public decimal Price { get; }

    public OrderItem(int productId, int quantity, decimal price)
    {
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }
}