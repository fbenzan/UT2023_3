using BenzanFF.Data.Constants;
using BenzanFF.Data.Response;

namespace BenzanFF.Data.Entities;

public class Imagen
{
    public int Id { get; set; }
    public string DatosBase64 { get; set; } = Imagenes.DefaultFile;
    public ImagenResponse ToResponse() => new() { Id = Id, DatosBase64 = DatosBase64 };
}
