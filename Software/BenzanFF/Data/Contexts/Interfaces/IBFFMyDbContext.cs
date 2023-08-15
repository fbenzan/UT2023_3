using BenzanFF.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BenzanFF.Data.Contexts.Interfaces
{
    public interface IBFFMyDbContext
    {
        DbSet<Imagen> Imagenes { get; set; }
        DbSet<ServicioCategoria> Categotias { get; set; }
        DbSet<Cliente> Clientes { get; set; }
        DbSet<Servicio> Servicios { get; set; }
        DbSet<Usuario> Usuarios { get; set; }
        DbSet<Venta> Ventas { get; set; }
        DbSet<VentaDetalle> VentasDetalles { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}