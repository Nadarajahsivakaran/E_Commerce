
using System.ComponentModel.DataAnnotations;

namespace Auth.Models.DTO
{
    public class UserDTO
    {
        [ Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        public string? Role { get; set; } = string.Empty;
    }
}
