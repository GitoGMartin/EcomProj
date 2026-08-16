using EcomProj.DTOs;

namespace EcomProj.Interfaces
{
    public interface IAuthService
    {
        Task<Guid> RegisterAsync(CreateUserDTO user);

        //Task<bool> Login(LoginDto dto);

        /*internal async Task<bool> Login(LoginDTO dto)
        {
            throw new NotImplementedException();
        }*/

    }
}
