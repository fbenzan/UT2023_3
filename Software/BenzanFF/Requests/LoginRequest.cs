using System.ComponentModel.DataAnnotations;

namespace BenzanFF.Requests;

public class LoginRequest
{
    [Required(ErrorMessage = "El usuario es obligatorio.")]
    public required string nickname { get; set; }
    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    public required string password { get; set; }
}
