using System.ComponentModel.DataAnnotations;

namespace BenzanFF.Data.Entities;

public class Cliente : Entity
{
    [MaxLength(150)]
    public string Nombre { get; set; } = null!;
    [MaxLength(20)]
    public string Telefono { get; set; } = null!;
    #region Relaciones
    public virtual ICollection<Venta>? Ventas { get; set; }
    #endregion
}
