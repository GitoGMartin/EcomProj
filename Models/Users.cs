namespace EcomProj.Models
{
    public class Users
    {
        private Guid userId {  get; set; }
        private string name { get; set; }
        private string Surname { get; set; }
        private string Email { get; set; }
        private string passwordHash { get; set; }
        private string phoneNumber { get; set; }
        private Boolean isActive { get; set; }
        private DateTime createdAt { get; set; }
        private DateTime updatedAt { get; set; }

    }
}
