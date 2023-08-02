using System.ComponentModel.DataAnnotations.Schema;

namespace BenzanFF.Data.Entities;

public class Servicio : Entity
{
    public int IdCategoria { get; set; }
    public string Descripcion { get; set; } = null!;
    public string Portada { get; set; } = null!;
    [Column(TypeName = "decimal(18,2)")]
    public decimal Precio { get; set; }

    #region Relaciones
    [ForeignKey(nameof(IdCategoria))]
    public ServicioCategoria? Categoria { get; set; }
    #endregion
}

