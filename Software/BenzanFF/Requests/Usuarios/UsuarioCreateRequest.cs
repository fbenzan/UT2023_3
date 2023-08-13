namespace BenzanFF.Requests.Usuarios;

public class UsuarioCreateRequest
{
    public string Name { get; set; } = "";
    public string Nickname { get; set; } = "";
    public string Password { get; set; } = "";
}
