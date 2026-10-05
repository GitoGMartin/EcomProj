namespace ECommerce.API.Models;

public class OrderItem
{
    public Guid orderitemId { get; set; }
    public Guid orderId { get; set; }
    public Guid productId { get; set; }
    public int quantity { get; set; }
    public decimal price { get; set; }
}