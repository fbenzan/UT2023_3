namespace BenzanFF.Data.Response;

public class CategoriaResponse
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
}
public class CategoriaResponseWithServiciosCount: CategoriaResponse
{
    public int Servicios { get; set; } = 0;
}
