using System.ComponentModel.DataAnnotations.Schema;

namespace BenzanFF.Data.Entities;

public class Venta : Entity
{
    public int IdCliente { get; set; }
    public DateTime Fecha { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Monto { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Recibido { get; set; }

    #region Relaciones
    [ForeignKey(nameof(IdCliente))]
    public Cliente Cliente { get; set; }
    public virtual ICollection<VentaDetalle> Detalles { get; set; }
    #endregion
}

