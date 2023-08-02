namespace BenzanFF.Data.Entities;

public class Cliente : Entity
{
    public string Nombre { get; set; } = null!;
    public string Telefono { get; set; } = null!;
    #region Relaciones
    public virtual ICollection<Venta>? Ventas { get; set; }
    #endregion
}

