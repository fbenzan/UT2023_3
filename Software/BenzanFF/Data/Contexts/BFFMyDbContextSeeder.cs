using BenzanFF.Data.Entities;

namespace BenzanFF.Data.Contexts;
//Esta clase es para inicializar datos en la base de datos.
public class BFFMyDbContextSeeder
{
    public static async Task Inicializar(BFFMyDbContext db)
    {
        await GenerarCategorias(db);
    }

    private static async Task GenerarCategorias(BFFMyDbContext db)
    {
        //En caso de no existir categorías, agregar las categorías por defecto.
        if (!db.Categotias.Any())
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
            await db.SaveChangesAsync();
        }
    }
}
