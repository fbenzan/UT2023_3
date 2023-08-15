using BenzanFF.Data.Constants;

namespace BenzanFF.Data.Response;

public class ImagenResponse
{
    public int Id { get; set; }
    public string DatosBase64 { get; set; } = Imagenes.DefaultFile;
    public string ToDisplayIMG => $"data:image/png;base64,{DatosBase64}";
}