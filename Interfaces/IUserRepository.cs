using ECommerce.API.Models;
using EcomProj.DTOs;

namespace ECommerce.API.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<UserDTO>> GetAllAsync();
    Task<UserDTO?> GetByIdAsync(Guid id);
    Task<User?> GetUserByEmail(string email);
    Task<Guid> CreateAsync(User user);
    Task<bool> UpdateAsync(Guid id, UserDTO user);
    Task<bool> DeleteAsync(Guid id);
}
