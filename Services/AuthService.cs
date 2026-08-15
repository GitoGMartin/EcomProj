using ECommerce.API.Interfaces;
using ECommerce.API.Models;
using EcomProj.DTOs;
using EcomProj.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace EcomProj.Services
{
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

        public async Task<bool> Login(LoginDTO dto)
        {
            User? user = await _userRepository.GetUserByEmail(dto.Email);

            if (user == null)
            {
                return false;
            }

            PasswordVerificationResult result =
                _passwordHasher.VerifyHashedPassword(
                    user,
                    user.passwordHash,
                    dto.Password
                );

            return result != PasswordVerificationResult.Failed;
        }

    }
}
