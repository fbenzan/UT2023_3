using BenzanFF.Data.Contexts.Interfaces;
using BenzanFF.Data.Entities;
using BenzanFF.Data.Requests;
using BenzanFF.Data.Response;
using Microsoft.EntityFrameworkCore;

namespace BenzanFF.Data.Services;

using SingleResponse = Result<CategoriaResponse>;
using ListResponse = ResultList<CategoriaResponseWithServiciosCount>;

public interface ICategoriaService
{
    Task<SingleResponse> CreateAsync(CategoriaRequest request);
    Task<Result> DeleteAsync(int Id);
    Task<ListResponse> GetAsync();
    Task<SingleResponse> GetAsync(int Id);
}

public class CategoriaService : ICategoriaService
{
    private readonly IBFFMyDbContext dbContext;
    private readonly ICurrentUserService currentUserService;

    public CategoriaService(IBFFMyDbContext dbContext, ICurrentUserService currentUserService)
    {
        this.dbContext = dbContext;
        this.currentUserService = currentUserService;
    }
    public async Task<SingleResponse> CreateAsync(CategoriaRequest request)
    {
        try
        {
            var categoria = ServicioCategoria.Crear(request);
            dbContext.Categotias.Add(categoria);
            await dbContext.SaveChangesAsync();
            return SingleResponse.Successed(categoria.ToResponse(), "La categoría se ha registrado exitosamente.");
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
            var categorias = (await dbContext.Categotias
                .Include(c => c.Servicos)
                .ToListAsync())
                .Select(c => c.ToResponseWithNumberServices())
                .ToList();
            if (categorias == null) return ListResponse.Failed("No hay categorias registradas...");

            return ListResponse.Successed(categorias);
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
            var categoria = await dbContext.Categotias
                .Include(s => s.Servicos)
                .FirstOrDefaultAsync(s => s.Id == Id);

            if (categoria == null)
                return SingleResponse.Failed("La categoria requerida no fue encontrada.");

            return SingleResponse.Successed(categoria.ToResponse());
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
            var s_to_delete = await dbContext.Categotias.FirstOrDefaultAsync(s => s.Id == Id);
            if (s_to_delete == null) return SingleResponse.Failed("No fue posible encontrar la categoría a eliminar...");
            //Representa el usuario que esta solicitanto la eliminacion.
            var usuario_operador = await dbContext.Usuarios.FirstOrDefaultAsync(user => user.Id == CurrentUserId);
            if (usuario_operador == null) return SingleResponse.Failed("No fue posible encontrar el usuario operador...");
            //Solo si el usuario que solicita el cambio de rol es administrador, se procede a realizar el cambio.
            if (usuario_operador.IsAdmin())
            {
                dbContext.Categotias.Remove(s_to_delete);
                await dbContext.SaveChangesAsync();
                return SingleResponse.Successed(s_to_delete.ToResponse(), "Se ha eliminado la categoría exitosamente...");
            }
            //Se retorna fallido si no es administrador.
            return SingleResponse.Failed("Usted no está autorizado para eliminar una categoría.");
        }
        catch (Exception e)
        {
            return ListResponse.Failed(e.Message);
        }
    }
}
