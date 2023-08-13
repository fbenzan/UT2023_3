namespace BenzanFF.Requests.Usuarios;

public class UsuarioChangePasswordRequest
{
    public int Id { get; set; }
    public string OldPassword { get; set; } = "";
    public string NewPassword { get; set; } = "";
    public string NewPasswordConfirm { get; set; } = "";
    public bool Change => (NewPassword == NewPasswordConfirm && !string.IsNullOrWhiteSpace(OldPassword));
    public void Reset()
    {
        OldPassword = "";
        NewPassword = "";
        NewPasswordConfirm = "";
    }
}
