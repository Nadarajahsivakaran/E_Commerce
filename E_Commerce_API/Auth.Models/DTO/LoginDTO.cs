
namespace Auth.Models.DTO
{
    public class LoginDTO 
    {
        public UserDTO? UserDTO { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
