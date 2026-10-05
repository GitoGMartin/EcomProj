namespace ECommerce.API.Models;

public class Inventory
{
    public Guid inventoryId { get; set; }
    public int quantity { get; set; }
    public int reservedQuantity { get; set; }
    public int reoderLevel { get; set; }
    public DateTime lastUpdateAt { get; set; } = DateTime.UtcNow;
}