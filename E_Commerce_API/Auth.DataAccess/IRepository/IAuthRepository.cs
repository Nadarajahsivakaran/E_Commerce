using Auth.Models;

namespace Auth.DataAccess.IRepository
{
    public interface IAuthRepository
    {
        Task<ServiceResponse> Register(Register register);

        Task<ServiceResponse> Login(Login login);
    }
}
