namespace ECommerce.API.Models;

public class Address
{
    public Guid addressId { get; set; }
    public Guid userId { get; set; }
    public string addressLine1 { get; set; }
    public string? addressLine2 { get; set; }
    public string city { get; set; }
    public string province { get; set; }
    public string postalCode { get; set; }
    public string country { get; set; }
    public Boolean isDefault { get; set; }
}