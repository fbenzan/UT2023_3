using BenzanFF.Extentions;
using BenzanFF.Response;

namespace BenzanFF.Data.Entities;

public class Usuario : Entity
{
    public Usuario()
    {
        
    }
    public Usuario(string name, string nickname, string password)
    {
        Name = name;
        Nickname = nickname;
        Password = password.Encriptar();
        Role = "basic";
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
    public string Role { get; set; } = null!;
    public static Usuario Crear(string name, string nickname, string password) =>
        new(name,nickname,password);
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
}

