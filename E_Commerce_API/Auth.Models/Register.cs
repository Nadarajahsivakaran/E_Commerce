using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Auth.Models
{
    public class Register
    {
        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "First Name must be between 6 and 30 characters.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Email must be between 6 and 30 characters.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required.")]
        [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 30 characters.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "Password must contain at least one uppercase letter and one number.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm Password is required.")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Role is required.")]
        [JsonConverter(typeof(JsonStringEnumConverter))] // Makes Swagger show string values instead of numbers
        public Role Roles { get; set; } = Role.USER;
    }
}
