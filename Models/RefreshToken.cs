namespace ECommerce.API.Models;

public class RefreshToken
{
    public Guid RefreshTokenId { get; set; }
    public Guid userId { get; set; }
    public string token { get; set; }
    public DateTime? expiresOn { get; set; }
    public Boolean? revoked { get; set; }
    public DateTime createDate { get; set; }
}