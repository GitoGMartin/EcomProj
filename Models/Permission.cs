namespace ECommerce.API.Models;

public class Permission
{
    public Guid PermissionId { get; set; }
    public string PermissionName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
