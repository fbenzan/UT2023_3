namespace BenzanFF.Data.Entities;

public class Usuario : Entity
{
    public string Name { get; set; } = null!;
    public string Nickname { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Role { get; set; } = null!;
}

