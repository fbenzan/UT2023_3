using BenzanFF.Requests;
using BenzanFF.Requests.Usuarios;
using BenzanFF.Response;

namespace BenzanFF.Data.Services
{
    public interface IUserManagerService
    {
        Task<Result<LoginResponse>> ChangePassword(UsuarioChangePasswordRequest request);
        Task<Result<LoginResponse>> ChangeRole(UsuarioChangeRoleRequest request);
        Task<Result> Create(UsuarioCreateRequest request);
        Task<ResultList<LoginResponse>> GetAsync();
        Task<Result<LoginResponse>> Login(LoginRequest request);
        Task<Result> Logout();
        Task<Result<LoginResponse>> Update(UsuarioRequest request);
    }
}