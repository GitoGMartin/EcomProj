using ECommerce.API.Interfaces;
using ECommerce.API.Models;
using EcomProj.DTOs;
using EcomProj.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;

namespace ECommerce.API.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        IUserRepository userRepository,
        IPasswordHasher<User> passwordHasher,
        ILogger<AuthService> logger)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _logger = logger;
    }
    private static string GetPasswordFingerprint(string password)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(password);
        byte[] hash = SHA256.HashData(bytes);

        return Convert.ToHexString(hash);
    }
    public async Task<Guid> RegisterAsync(CreateUserDTO dto)
    {
        User? existingUser = await _userRepository.GetUserByEmail(dto.Email);

        if (existingUser != null)
        {
            _logger.LogWarning(
                "Registration failed: user already exists for email {Email}",
                dto.Email
            );

            return Guid.Empty;
        }

        User user = new User
        {
            UserId = Guid.NewGuid(),
            FirstName = dto.firstName,
            LastName = dto.lastName,
            Email = dto.Email,
            phoneNumber = dto.PhoneNumber,
            isActive = true,
            createDate = DateTime.UtcNow,
            updateDate = DateTime.UtcNow
        };

        // Hash the password
        user.passwordHash = _passwordHasher.HashPassword(
            user,
            dto.Password
        );





        // Save user to database
        Guid userId = await _userRepository.CreateAsync(user);

        if (userId == Guid.Empty)
        {
            _logger.LogError(
                "Failed to create user in database for {Email}",
                dto.Email
            );

            return Guid.Empty;
        }


        return userId;
    }

    public async Task<bool> Login(LoginDTO dto)
    {
        string email = dto.Email ?? string.Empty;


        User? user = await _userRepository.GetUserByEmail(email);

        if (user == null)
        {
            _logger.LogWarning(
                "USER NOT FOUND"
            );

            return false;
        }


        PasswordVerificationResult databaseHashResult =
            _passwordHasher.VerifyHashedPassword(
                user,
                user.passwordHash,
                dto.Password
            );



        if (databaseHashResult == PasswordVerificationResult.Success)
        {
            _logger.LogWarning("LOGIN SUCCESS");
            return true;
        }

        _logger.LogWarning("LOGIN FAILED");

        return false;
    }
}