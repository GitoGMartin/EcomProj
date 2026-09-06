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
    private readonly IJwtService _jwtService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public AuthService(
        IUserRepository userRepository,
        IPasswordHasher<User> passwordHasher,
        IJwtService jwtService, IRefreshTokenRepository refreshTokenRepository)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
        _refreshTokenRepository = refreshTokenRepository;
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


            return Guid.Empty;
        }


        return userId;
    }

    public async Task<AuthResponseDTO?> Login(LoginDTO dto)
    {
        string email = dto.Email ?? string.Empty;


        User? user = await _userRepository.GetUserByEmail(email);

        if (user == null)
        {


            return null;
        }


        PasswordVerificationResult databaseHashResult =
            _passwordHasher.VerifyHashedPassword(
                user,
                user.passwordHash,
                dto.Password
            );



        if (databaseHashResult != PasswordVerificationResult.Success)
        {


            return null;
        }
        UserDTO dtoUser = new UserDTO
        {
            id = user.UserId,
            firstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.phoneNumber,
            IsActive = user.isActive,
            CreateDate = user.createDate,
            UpdateDate = user.updateDate
        };

        var accessToken = _jwtService.GenerateAccessToken(dtoUser);
        var refreshToken = _jwtService.GenerateRefreshToken();

        var refreshTokenEntity = new RefreshToken
        {
            userId = user.UserId,
            token = refreshToken,
            createDate = DateTime.UtcNow,
            expiresOn = DateTime.UtcNow.AddDays(7)
        };

        await _refreshTokenRepository.Create(refreshTokenEntity);

        return new AuthResponseDTO
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresIn = 900
        };


    }
    public async Task<AuthResponseDTO?> RefreshToken(string refreshToken)
    {
        var storedToken = await _refreshTokenRepository.GetByToken(refreshToken);

        if (storedToken == null)
        {
            return null;
        }

        if (storedToken.revoked != null)
        {
            return null;
        }

        if (storedToken.expiresOn <= DateTime.UtcNow)
        {
            return null;
        }


        UserDTO? user = await _userRepository.GetByIdAsync(storedToken.userId);

        if (user == null)
        {
            return null;
        }
        var accessToken = _jwtService.GenerateAccessToken(user);

        return new AuthResponseDTO
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresIn = 900
        };
    }
}