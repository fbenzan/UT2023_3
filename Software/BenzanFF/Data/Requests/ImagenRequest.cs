using BenzanFF.Data.Constants;

namespace BenzanFF.Data.Requests;

public class ImagenRequest
{
    public string DatosBase64 { get; set; } = Imagenes.DefaultFile;
    public string ToDisplayIMG => $"data:image/png;base64,{DatosBase64}";
}
