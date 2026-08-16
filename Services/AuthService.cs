using ECommerce.API.Interfaces;
using ECommerce.API.Models;
using EcomProj.DTOs;
using EcomProj.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.API.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AuthService(
        IUserRepository userRepository,
        IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> RegisterAsync(CreateUserDTO dto)
    {
        User? existingUser = await _userRepository.GetUserByEmail(dto.Email);

        if (existingUser != null)
        {
            return Guid.Empty;
        }

        User user = new User
        {
            UserId = Guid.NewGuid(),
            FirstName = dto.firstName,
            LastName = dto.LastName,
            Email = dto.Email,
            phoneNumber = dto.PhoneNumber,
            isActive = true,
            createDate = DateTime.UtcNow,
            updateDate = DateTime.UtcNow
        };

        user.passwordHash = _passwordHasher.HashPassword(
            user,
            dto.Password
        );

        return await _userRepository.CreateAsync(user);
    }
}