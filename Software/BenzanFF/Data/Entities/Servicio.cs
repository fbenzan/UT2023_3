using BenzanFF.Data.Requests;
using BenzanFF.Data.Response;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenzanFF.Data.Entities;

public class Servicio : Entity
{
    public int IdCategoria { get; set; }
    public string Descripcion { get; set; } = null!;
    public int PortadaId { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Precio { get; set; }

    #region Relaciones
    [ForeignKey(nameof(IdCategoria))]
    public ServicioCategoria? Categoria { get; set; }
    [ForeignKey(nameof(PortadaId))]
    public Imagen? Portada { get; set; }
    #endregion

    public static Servicio Crear(ServicioRequest request) => new()
    {
        Descripcion = request.Descripcion,
        Precio = request.Precio,
        Portada = new Imagen() { Id = 0, DatosBase64 = request.imagen.DatosBase64 },
        IdCategoria = request.IdCategoria
    };

    public ServicioResponse ToResponse() => new()
    {
        Id = Id,
        Categoria = Categoria!.ToResponse(),
        Descripcion = Descripcion,
        Portada = Portada!.ToResponse(),
        Precio = Precio
    };

}

