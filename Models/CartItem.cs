namespace ECommerce.API.Models;

public class CartItem
{
    public Guid cartItemId { get; set; }
    public Guid cartId { get; set; }
    public Guid productId { get; set; }
    public int quantity { get; set; }
}