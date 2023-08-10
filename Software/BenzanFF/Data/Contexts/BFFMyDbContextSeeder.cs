using BenzanFF.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace BenzanFF.Data.Contexts;
//Esta clase es para inicializar datos en la base de datos.
public class BFFMyDbContextSeeder
{
    public static async Task Inicializar(BFFMyDbContext db)
    {
        var safe_categorias = await GenerarCategorias(db);
        var safe_usuario_admin = await GenerarUsuarioAdmin(db);
        if(safe_categorias || safe_usuario_admin) 
            await db.SaveChangesAsync();
    }

    private static async Task<bool> GenerarCategorias(BFFMyDbContext db)
    {
        //En caso de no existir categorías, agregar las categorías por defecto.
        var existen_categorias = await db.Categotias.AnyAsync();
        if (!existen_categorias)
        {
            //Variables para las categorias nuevas.
            var categorias_nuevas = new List<ServicioCategoria>()
            {
                new (){ Nombre = "Guarniciones"},
                new (){ Nombre = "Carnes"},
                new (){ Nombre = "Hamburguesas"},
            };
            //Se agregar a la tabla del contexto de la base de datos.
            db.Categotias.AddRange(categorias_nuevas);
            //Se guardan los cambios en la base de datos.
            return true;//Forzar un safe change unico...
        }
        return false;//Evitar un safe change unico...
    }
    private static async Task<bool> GenerarUsuarioAdmin(BFFMyDbContext db)
    {
        var role = "Administrator";
        var admin = await db.Usuarios.FirstOrDefaultAsync(user => user.Role == role);
        if (admin==null)
        {
            admin = Usuario.Crear("Felix Benzan", "fbenzan", "123", "Administrator");
            db.Usuarios.Add(admin);
            return true;//Forzar un safe change unico...
        }
        return false;//Evitar un safe change unico...
    }
}
