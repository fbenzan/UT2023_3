using BenzanFF.Data.Contexts.Interfaces;
using BenzanFF.Data.Entities;
using BenzanFF.Data.Requests;
using BenzanFF.Data.Response;
using Microsoft.EntityFrameworkCore;

namespace BenzanFF.Data.Services;

using SingleResponse = Result<ServicioResponse>;
using ListResponse = ResultList<ServicioResponse>;

public interface IServiciosService
{
    Task<SingleResponse> CreateAsync(ServicioRequest request);
    Task<Result> DeleteAsync(int Id);
    Task<ListResponse> GetAsync();
    Task<SingleResponse> GetAsync(int Id);
}

public class ServiciosService : IServiciosService
{
    private readonly IBFFMyDbContext dbContext;
    private readonly ICurrentUserService currentUserService;

    public ServiciosService(IBFFMyDbContext dbContext, ICurrentUserService currentUserService)
    {
        this.dbContext = dbContext;
        this.currentUserService = currentUserService;
    }
    public async Task<SingleResponse> CreateAsync(ServicioRequest request)
    {
        try
        {
            var servicio = Servicio.Crear(request);
            dbContext.Servicios.Add(servicio);
            await dbContext.SaveChangesAsync();
            return SingleResponse.Successed(servicio.ToResponse(), "El servicio se ha registrado exitosamente.");
        }
        catch (Exception e)
        {
            return SingleResponse.Failed(e.Message);
        }
    }
    public async Task<ListResponse> GetAsync()
    {
        try
        {
            var users = (await dbContext.Servicios
                .Include(s => s.Portada)
                .Include(s => s.Categoria)
                .ToListAsync())
                .Select(user => user.ToResponse())
                .ToList();
            if (users == null) return ListResponse.Failed("No hay servicios registrados...");

            return ListResponse.Successed(users);
        }
        catch (Exception e)
        {
            return ListResponse.Failed(e.Message);
        }
    }
    public async Task<SingleResponse> GetAsync(int Id)
    {
        try
        {
            var servicio = await dbContext.Servicios
                .Include(s => s.Portada)
                .Include(s => s.Categoria)
                .FirstOrDefaultAsync(s => s.Id == Id);

            if (servicio == null)
                return SingleResponse.Failed("El servicio requerido no fue encontrado.");

            return SingleResponse.Successed(servicio.ToResponse());
        }
        catch (Exception e)
        {
            return SingleResponse.Failed(e.Message);
        }
    }
    public async Task<Result> DeleteAsync(int Id)
    {
        try
        {
            var CurrentUserId = await currentUserService.UserId();
            //Representa el servicio que se eliminara.
            var s_to_delete = await dbContext.Servicios.FirstOrDefaultAsync(s => s.Id == Id);
            if (s_to_delete == null) return SingleResponse.Failed("No fue posible encontrar el servicio a eliminar...");
            //Representa el usuario que esta solicitanto la eliminacion.
            var usuario_operador = await dbContext.Usuarios.FirstOrDefaultAsync(user => user.Id == CurrentUserId);
            if (usuario_operador == null) return SingleResponse.Failed("No fue posible encontrar el usuario operador...");
            //Solo si el usuario que solicita el cambio de rol es administrador, se procede a realizar el cambio.
            if (usuario_operador.IsAdmin())
            {
                dbContext.Servicios.Remove(s_to_delete);
                await dbContext.SaveChangesAsync();
                return SingleResponse.Successed(s_to_delete.ToResponse(), "Se ha eliminado el servicio exitosamente...");
            }
            //Se retorna fallido si no es administrador.
            return SingleResponse.Failed("Usted no está autorizado para eliminar un servicios.");
        }
        catch (Exception e)
        {
            return ListResponse.Failed(e.Message);
        }
    }
}
