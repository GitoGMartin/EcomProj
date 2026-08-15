using EcomProj.DTOs;

namespace EcomProj.Interfaces
{
    public class IAuthService
    {
        Task<bool> Register(RegisterDto dto);

        Task<bool> Login(LoginDto dto);

        internal async Task<bool> Login(LoginDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
