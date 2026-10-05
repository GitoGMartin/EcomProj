namespace ECommerce.API.Models;

public class Category
{
    public Guid CategoryId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public Guid? ParentCategoryId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}