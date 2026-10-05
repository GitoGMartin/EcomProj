namespace ECommerce.API.Models;

public class InventoryTransaction
{
    public Guid transactionId { get; set; }
    public Guid productId { get; set; }
    public int quantity { get; set; }
    public required string transactionType { get; set; } // "IN" for incoming, "OUT" for outgoing
    public Guid referenceId { get; set; } // Could be an orderId, purchaseId, etc.
    public string? notes { get; set; }
    public DateTime transactionDate { get; set; } = DateTime.UtcNow;
}