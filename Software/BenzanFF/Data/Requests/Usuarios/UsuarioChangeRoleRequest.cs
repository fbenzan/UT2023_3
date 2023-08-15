namespace BenzanFF.Data.Requests.Usuarios;

public class UsuarioChangeRoleRequest
{
    public int IdUserToChange { get; set; }
    public int IdCurrentUser { get; set; }
    public string Role { get; set; } = Constants.Roles.Basic;
}
