using EcomProj.DTOs;

namespace EcomProj.Interfaces
{
    public interface IJwtService
    {
        string GenerateAccessToken(UserDTO user);
        string GenerateRefreshToken();
    }
}
