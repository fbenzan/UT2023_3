using BenzanFF.Authentication;
using BenzanFF.Data.Contexts.Interfaces;
using BenzanFF.Data.Entities;
using BenzanFF.Extentions;
using BenzanFF.Requests;
using BenzanFF.Requests.Usuarios;
using BenzanFF.Response;
using Microsoft.EntityFrameworkCore;

namespace BenzanFF.Data.Services;
//Se declara el SingleResponse para una respuesta simple de un solo dato...
using SingleResponse = Result<LoginResponse>;
using ListResponse = ResultList<LoginResponse>;

public class UserManagerService : IUserManagerService
{
    private readonly IBFFMyDbContext dbContext;
    private readonly ICustomAuthenticationStateProvider customAuthentication;
    private readonly ICurrentUserService currentUserService;

    public UserManagerService(IBFFMyDbContext dbContext, ICustomAuthenticationStateProvider customAuthentication, ICurrentUserService currentUserService)
    {
        this.dbContext = dbContext;
        this.customAuthentication = customAuthentication;
        this.currentUserService = currentUserService;
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
            if (!user.Authorized(request.password)) return SingleResponse.Failed(sms_fail);
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
            return Result.Failed(E.Message);
        }
    }
    //Crear usuarios basicos...
    public async Task<Result> Create(UsuarioCreateRequest request)
    {
        try
        {
            var user = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Nickname == request.Nickname);
            if (user != null) return Result.Failed($"Ya existe un usuario con el seudonimo '{request.Nickname}'.");
            user = Usuario.Crear(request);
            dbContext.Usuarios.Add(user);
            await dbContext.SaveChangesAsync();
            return Result.Successed("Usuario creado exitosamente!");
        }
        catch (Exception E)
        {
            return Result.Failed(E.Message);
        }
    }
    //Modificar usuarios...
    public async Task<SingleResponse> Update(UsuarioRequest request)
    {
        try
        {
            var usuario = await dbContext.Usuarios.FirstOrDefaultAsync(user => user.Id == request.Id);
            if (usuario == null) return SingleResponse.Failed("No fue posible encontrar el usuario indiado...");
            if (usuario.Name != request.Name)
            {
                usuario.Name = request.Name;
                await dbContext.SaveChangesAsync();
                var model = usuario.ToResponse();
                await customAuthentication.UpdateAuthenticationState(model);
                return SingleResponse.Successed(model, "Se ha cambiado el nombre del usuario exitosamente...");
            }
            return SingleResponse.Failed("No se realizó, ningún cambio...");
        }
        catch (Exception E)
        {
            return SingleResponse.Failed(E.Message);
        }
    }
    //Cambiar contrasena
    public async Task<SingleResponse> ChangePassword(UsuarioChangePasswordRequest request)
    {
        try
        {
            var usuario = await dbContext.Usuarios.FirstOrDefaultAsync(user => user.Id == request.Id);
            if (usuario == null) return SingleResponse.Failed("No fue posible encontrar el usuario indiado...");
            if (usuario.Authorized(request.OldPassword))
            {
                usuario.Password = request.NewPassword.Encriptar();
                await dbContext.SaveChangesAsync();
                return SingleResponse.Successed(usuario.ToResponse(), "Se ha cambiado la contraseña exitosamente...");
            }
            return SingleResponse.Failed("Su contraseña no coincide...");

        }
        catch (Exception E)
        {
            return SingleResponse.Failed(E.Message);
        }
    }
    //Cambiar de rol
    public async Task<SingleResponse> ChangeRole(UsuarioChangeRoleRequest request)
    {
        try
        {
            var CurrentUserId = await currentUserService.UserId();
            //Representa el usuario al que se le cambiara el rol.
            var usuario_beneficiado = await dbContext.Usuarios.FirstOrDefaultAsync(user => user.Id == request.IdUserToChange);
            if (usuario_beneficiado == null) return SingleResponse.Failed("No fue posible encontrar el usuario a cambiar...");
            //Representa el usuario que esta solicitanto el cambio de rol.
            var usuario_operador = await dbContext.Usuarios.FirstOrDefaultAsync(user => user.Id == CurrentUserId);
            if (usuario_operador == null) return SingleResponse.Failed("No fue posible encontrar el usuario operador...");
            //Solo si el usuario que solicita el cambio de rol es administrador, se procede a realizar el cambio.
            if (usuario_operador.IsAdmin())
            {
                usuario_beneficiado.Role = request.Role;
                await dbContext.SaveChangesAsync();
                return SingleResponse.Successed(usuario_beneficiado.ToResponse(), "Se ha cambiado la contraseña exitosamente...");
            }
            //Se retorna fallido si no es administrador.
            return SingleResponse.Failed("Usted no está autorizado para realizar el cambio de rol.");

        }
        catch (Exception E)
        {
            return SingleResponse.Failed(E.Message);
        }
    }
    //Listar los usuarios existentes
    public async Task<ListResponse> GetAsync()
    {
        try
        {
            var users = (await dbContext.Usuarios.ToListAsync())
                .Select(user => user.ToResponse())
                .ToList();
            if (users == null) return ListResponse.Failed("No hay usuarios registrados...");

            return ListResponse.Successed(users);
        }
        catch (Exception e)
        {
            return ListResponse.Failed(e.Message);
        }
    }
    //Listar los usuarios existentes
    public async Task<Result> Delete(int Id)
    {
        try
        {
            var CurrentUserId = await currentUserService.UserId();
            //Representa el usuario al que se le cambiara el rol.
            var user_to_delete = await dbContext.Usuarios.FirstOrDefaultAsync(user => user.Id == Id);
            if (user_to_delete == null) return SingleResponse.Failed("No fue posible encontrar el usuario a cambiar...");
            //Representa el usuario que esta solicitanto el cambio de rol.
            var usuario_operador = await dbContext.Usuarios.FirstOrDefaultAsync(user => user.Id == CurrentUserId);
            if (usuario_operador == null) return SingleResponse.Failed("No fue posible encontrar el usuario operador...");
            //Solo si el usuario que solicita el cambio de rol es administrador, se procede a realizar el cambio.
            if (usuario_operador.IsAdmin())
            {
                dbContext.Usuarios.Remove(user_to_delete);
                await dbContext.SaveChangesAsync();
                return SingleResponse.Successed(user_to_delete.ToResponse(), "Se ha eliminado el usuario exitosamente...");
            }
            //Se retorna fallido si no es administrador.
            return SingleResponse.Failed("Usted no está autorizado para eliminar un usuario.");

        }
        catch (Exception e)
        {
            return ListResponse.Failed(e.Message);
        }
    }
}
