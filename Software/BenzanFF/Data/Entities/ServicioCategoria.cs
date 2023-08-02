namespace BenzanFF.Data.Entities;

public class ServicioCategoria : Entity
{
    public string Nombre { get; set; } = null!;

    #region Relaciones
    public virtual ICollection<Servicio>? Servicos { get; set; }
    #endregion
}

