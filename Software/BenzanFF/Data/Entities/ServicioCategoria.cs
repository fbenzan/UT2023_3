using BenzanFF.Data.Requests;
using BenzanFF.Data.Response;

namespace BenzanFF.Data.Entities;

public class ServicioCategoria : Entity
{
    public string Nombre { get; set; } = null!;

    #region Relaciones
    public virtual ICollection<Servicio>? Servicos { get; set; }

    public static ServicioCategoria Crear(CategoriaRequest request)
    => new() { Nombre = request.Nombre };
    #endregion

    public CategoriaResponse ToResponse() => new() { Id = Id, Nombre = Nombre };
    public CategoriaResponseWithServiciosCount ToResponseWithNumberServices() => new() { Id = Id, Nombre = Nombre, Servicios = Servicos!=null? Servicos.Count:0 };
}

