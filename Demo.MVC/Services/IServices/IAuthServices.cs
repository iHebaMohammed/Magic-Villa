using Demo.API.DTOs;

namespace Demo.MVC.Services.IServices
{
    public interface IAuthServices
    {
        Task<T> LoginAsync<T>(LoginRequestDTO loginRequestDTO);
        Task<T> RegisterAsync<T>(RegisterationRequestDTO registerationRequestDTO);
    }
}
