using System.ComponentModel.DataAnnotations;

namespace BenzanFF.Data.Requests.Usuarios;

public class UsuarioCreateRequest
{
    [Required(ErrorMessage = "El nombre del usuario es obligatorio.")]
    public string Name { get; set; } = "";
    [Required(ErrorMessage = "El nickname del usuario es obligatorio.")]
    public string Nickname { get; set; } = "";
    [Required(ErrorMessage = "La contraseña del usuario es obligatoria."), DataType(DataType.Password), StringLength(8, MinimumLength = 4, ErrorMessage = "La contraseña debe tener entre 4 y 8 caracteres.")]
    public string Password { get; set; } = "";
}
