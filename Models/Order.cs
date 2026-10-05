namespace ECommerce.API.Models;

public class Order
{
    public Guid orderId { get; set; }
    public Guid userId { get; set; }
    public Guid addressId { get; set; }
    public Guid? couponId { get; set; }
    public DateTime orderDate { get; set; }
    public string? orderStatus { get; set; } = string.Empty;
    public float totalAmount { get; set; }
}