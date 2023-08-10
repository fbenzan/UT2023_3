using BenzanFF.Requests;
using BenzanFF.Response;

namespace BenzanFF.Data.Services
{
    public interface IUserManagerService
    {
        Task<Result<LoginResponse>> Login(LoginRequest request);
        Task<Result> Logout();
    }
}