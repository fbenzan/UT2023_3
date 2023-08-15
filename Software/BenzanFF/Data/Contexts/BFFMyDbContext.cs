
using BenzanFF.Data.Contexts.Interfaces;
using BenzanFF.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BenzanFF.Data.Contexts;
public class BFFMyDbContext : DbContext, IBFFMyDbContext
{
    private readonly IConfiguration configuration;

    /// <summary>
    /// Usamos el constructor de la clase para leer los parametros de coneccion del appsettings...
    /// </summary>
    /// <param name="configuration"></param>
    public BFFMyDbContext(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    /// <summary>
    /// Se configura la conexion a la base de datos
    /// </summary>
    /// <param name="optionsBuilder"></param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("MSSQL"));
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }
    #region Tablas
    public DbSet<Imagen> Imagenes { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Servicio> Servicios { get; set; }
    public DbSet<ServicioCategoria> Categotias { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Venta> Ventas { get; set; }
    public DbSet<VentaDetalle> VentasDetalles { get; set; }
    #endregion
}
