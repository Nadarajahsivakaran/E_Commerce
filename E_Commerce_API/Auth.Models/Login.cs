
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Auth.Models
{
    public class Login
    {
        [Required, StringLength(30), DisplayName("User Name")]
        public string UserName { get; set; } = string.Empty;

        [Required, StringLength(30)]
        public string Password { get; set; } = string.Empty;
    }
}
