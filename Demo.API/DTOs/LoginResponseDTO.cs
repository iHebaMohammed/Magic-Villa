using Demo.DAL.Entities.Identity;

namespace Demo.API.DTOs
{
    public class LoginResponseDTO
    {
        public UserDTO User { get; set; }
        public string Token { get; set; }
    }
}
