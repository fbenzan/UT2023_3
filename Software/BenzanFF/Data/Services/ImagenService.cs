using BenzanFF.Data.Contexts.Interfaces;
using BenzanFF.Data.Entities;

namespace BenzanFF.Data.Services;

public interface IImagenService
{
    Task<int> GuardarImagenAsync(string datosBase64);
    Task<string> ObtenerImagenAsync(int id);
}
public class ImagenService : IImagenService
{
    private readonly IBFFMyDbContext _context;

    public ImagenService(IBFFMyDbContext context)
    {
        _context = context;
    }

    public async Task<int> GuardarImagenAsync(string datosBase64)
    {
        var imagen = new Imagen { DatosBase64 = datosBase64 };
        _context.Imagenes.Add(imagen);
        await _context.SaveChangesAsync();
        return imagen.Id;
    }
    public async Task<string> ObtenerImagenAsync(int id)
    {
        var imagen = await _context.Imagenes.FindAsync(id);
        return imagen?.DatosBase64!;
    }
}

