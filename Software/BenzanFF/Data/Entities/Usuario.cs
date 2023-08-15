using BenzanFF.Data.Constants;
using BenzanFF.Data.Requests.Usuarios;
using BenzanFF.Data.Response;
using BenzanFF.Extentions;

namespace BenzanFF.Data.Entities;

public class Usuario : Entity
{
    public Usuario()
    {
        Role = Roles.Basic;
    }
    public Usuario(string name, string nickname, string password)
    {
        Name = name;
        Nickname = nickname;
        Password = password.Encriptar();
        Role = Roles.Basic;
    }
    public Usuario(string name, string nickname, string password, string role)
    {
        Name = name;
        Nickname = nickname;
        Password =  password.Encriptar();
        Role = role;
    }

    public string Name { get; set; } = null!;
    public string Nickname { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Role { get; set; } = Roles.Basic;
    public static Usuario Crear(UsuarioCreateRequest request) =>
        new(request.Name, request.Nickname, request.Password);
    public static Usuario Crear(string name, string nickname, string password,string role) =>
        new(name, nickname, password,role);
    public bool Authorized(string password)
    {
        return Password == password.Encriptar();
    }

    public LoginResponse ToResponse()
    => new() 
    { 
        Id = Id,
        Name = Name,
        Nickname = Nickname,
        Role = Role 
    };

    internal bool IsAdmin()
    {
        return Role == Constants.Roles.Admin;
    }
}

