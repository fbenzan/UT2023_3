namespace BenzanFF.Data.Response;

public class LoginResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Nickname { get; set; } = "";
    public string Role { get; set; } = "";
    public bool IsAdmin() => Role == Constants.Roles.Admin;
}