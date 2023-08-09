namespace BenzanFF.Response;

public class LoginResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Nickname { get; set; } = null!;
    public string Role { get; set; } = null!;
}
