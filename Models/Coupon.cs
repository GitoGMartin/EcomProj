namespace ECommerce.API.Models;

public class Coupon
{
    public Guid couponId { get; set; }
    public required string couponCode { get; set; }
    public float? discountPercentage { get; set; }
    public float? discountAmount { get; set; }
    public float? minimumPurchaseAmount { get; set; }
    public DateTime? expirationDate { get; set; }
    public bool isActive { get; set; }
}