using ECommerce.API.Models;

namespace EcomProj.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task Create(RefreshToken refreshToken);

        Task<RefreshToken?> GetByToken(string token);

        Task Revoke(int refreshTokenId);
    }
}
