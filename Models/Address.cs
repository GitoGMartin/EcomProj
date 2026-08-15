namespace ECommerce.API.Models;

public class Address
{
    private Guid addressId { get; set; }
    private Guid userId { get; set; }
    private string addressLine1 { get; set; }
    private string? addressLine2 { get; set; }
    private string city { get; set; }
    private string province { get; set; }
    private string postalCode { get; set; }
    private string country { get; set; }
    private Boolean isDefault { get; set; }
}