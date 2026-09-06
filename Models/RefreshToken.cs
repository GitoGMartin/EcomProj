namespace ECommerce.API.Models;

public class RefreshToken
{
    public int RefreshTokenId { get; set; }
    public Guid userId { get; set; }
    public string token { get; set; }
    public DateTime? expiresOn { get; set; }
    public DateTime? revoked { get; set; }
    public DateTime createDate { get; set; }
}