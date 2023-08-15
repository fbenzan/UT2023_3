namespace BenzanFF.Data.Response;

public class ServicioResponse
{
    public int Id { get; set; }
    public CategoriaResponse? Categoria { get; set; }
    public string Descripcion { get; set; } = null!;
    public decimal Precio { get; set; }
    public ImagenResponse? Portada { get; set; }
}
