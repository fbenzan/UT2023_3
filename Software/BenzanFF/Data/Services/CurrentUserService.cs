using BenzanFF.Response;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace BenzanFF.Data.Services;

public interface ICurrentUserService
{
    Task<string> Name();
    Task<string> Role();
    Task<int> UserId();
}

public class CurrentUserService : ICurrentUserService
{
    private readonly ProtectedSessionStorage _sessionStorage;
    public CurrentUserService(ProtectedSessionStorage sessionStorage)
    {
        _sessionStorage = sessionStorage;
    }

    private async Task<LoginResponse> GetUserDataAsync()
    {
        var userDataStorage = await _sessionStorage.GetAsync<LoginResponse>("UserToken");
        return userDataStorage!.Success ? userDataStorage!.Value! : new LoginResponse();
    }
    public async Task<int> UserId()
    {
        var userData = await GetUserDataAsync();
        return userData?.Id ?? 0;
    }
    public async Task<string> Name()
    {
        var userData = await GetUserDataAsync();
        return userData?.Name ?? "Guest";

    }
    public async Task<string> Role()
    {
        var userData = await GetUserDataAsync();
        return userData?.Role ?? "";

    }
}

