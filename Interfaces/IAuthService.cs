using EcomProj.DTOs;

namespace EcomProj.Interfaces
{
    public interface IAuthService
    {
        Task<Guid> RegisterAsync(CreateUserDTO user);

        Task<AuthResponseDTO?> Login(LoginDTO dto);



    }
}
