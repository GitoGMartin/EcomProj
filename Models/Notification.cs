namespace ECommerce.API.Models;

public class Notification
{
    public Guid notificationId { get; set; }
    public Guid userId { get; set; }
    public string title { get; set; } = string.Empty;
    public string message { get; set; } = string.Empty;
    public string type { get; set; } = string.Empty;
    public bool isRead { get; set; }
    public DateTime createdAt { get; set; }
}