
namespace Auth.Models
{
    public class ServiceResponse
    {
        public bool IsSuccess { get; set; } = true;

        public object? Res { get; set; } = null;
    }
}
