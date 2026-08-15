namespace ECommerce.API.Models;

public class User
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string passwordHash { get; set; }
    private string phoneNumber { get; set; }
    public Boolean isActive { get; set; }
    public DateTime createDate { get; set; }
    public DateTime updateDate { get; set; }
}