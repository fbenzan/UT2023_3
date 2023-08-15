using System.ComponentModel.DataAnnotations;

namespace BenzanFF.Data.Requests;

public class CategoriaRequest
{
    [Required(ErrorMessage = "El nombre de la categoría es obligatorio.")]
    public string Nombre { get; set; } = null!;
}
