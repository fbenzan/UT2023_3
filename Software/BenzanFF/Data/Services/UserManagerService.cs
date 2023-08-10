using BenzanFF.Authentication;
using BenzanFF.Data.Contexts.Interfaces;
using BenzanFF.Extentions;
using BenzanFF.Requests;
using BenzanFF.Response;
using Microsoft.EntityFrameworkCore;

namespace BenzanFF.Data.Services;
//Se declara el SingleResponse para una respuesta simple de un solo dato...
using SingleResponse = Result<LoginResponse>;

public class UserManagerService : IUserManagerService
{
    private readonly IBFFMyDbContext dbContext;
    private readonly ICustomAuthenticationStateProvider customAuthentication;

    public UserManagerService(IBFFMyDbContext dbContext, ICustomAuthenticationStateProvider customAuthentication)
    {
        this.dbContext = dbContext;
        this.customAuthentication = customAuthentication;
    }
    //El login devuelve una respuesta simple...
    public async Task<SingleResponse> Login(LoginRequest request)
    {
        try
        {
            //Para los casos fallidos, es este mensaje.
            var sms_fail = "Credenciales incorrectas";
            //Se busca el usuario en la base de datos
            var user = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Nickname == request.nickname);
            //Si no se encuentra el usuario, mesaje fallido.
            if (user == null) return SingleResponse.Failed(sms_fail);
            //Si no es la contrasena del usuario, mesaje fallido.
            if (user.Password != request.password.Encriptar()) return SingleResponse.Failed(sms_fail);
            //Si logra superar todas las validaciones devolvemos el loginResponse.
            var model = user.ToResponse();
            await customAuthentication.UpdateAuthenticationState(model);
            return SingleResponse.Successed(model);
        }
        catch (Exception E)
        {
            return SingleResponse.Failed(E.Message);
        }
    }
    public async Task<Result> Logout()
    {
        try
        {

            await customAuthentication.UpdateAuthenticationState(null!);
            return Result.Successed("Sesión cerrada exitosamente!");
        }
        catch (Exception E)
        {
            return SingleResponse.Failed(E.Message);
        }
    }
    //Crear usuarios basicos...

    //Modificar usuarios...

    //Cambiar contrasena

    //Cambiar de rol

}
