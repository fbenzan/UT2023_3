using System.ComponentModel.DataAnnotations;

namespace BenzanFF.Data.Requests.Usuarios;

public class UsuarioChangePasswordRequest
{
    public int Id { get; set; }
    [Required(ErrorMessage = "La contraseña actual del usuario es obligatoria."), DataType(DataType.Password)]
    public string OldPassword { get; set; } = "";
    [Required(ErrorMessage = "La contraseña del usuario es obligatoria."), DataType(DataType.Password), StringLength(8, MinimumLength = 4, ErrorMessage = "La nueva contraseña debe tener entre 4 y 8 caracteres.")]
    public string NewPassword { get; set; } = "";
    [Required(ErrorMessage = "La contraseña de confirmación del usuario es obligatoria."), DataType(DataType.Password), Compare("NewPassword", ErrorMessage = "Las contraseñas no coinciden")]
    public string NewPasswordConfirm { get; set; } = "";
    public bool Change => NewPassword == NewPasswordConfirm && !string.IsNullOrWhiteSpace(OldPassword);
    public void Reset()
    {
        OldPassword = "";
        NewPassword = "";
        NewPasswordConfirm = "";
    }
}
