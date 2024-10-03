using Demo.API.DTOs;
using Demo.DAL.Entities.Identity;

namespace Demo.API.Services.IServices
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string email);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<UserDTO> Register(RegisterationRequestDTO registerationRequestDTO);
    }
}
