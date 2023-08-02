using System.ComponentModel.DataAnnotations.Schema;

namespace BenzanFF.Data.Entities;

public class VentaDetalle : Entity
{
    public int IdVenta { get; set; }
    public int IdServicio { get; set; }
    public int Cantidad { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal PrecioVenta { get; set; }
    #region Relaciones
    [ForeignKey(nameof(IdVenta))]
    public Venta Venta { get; set; }

    [ForeignKey(nameof(IdServicio))]
    public Servicio Servicio { get; set; }
    #endregion
}

