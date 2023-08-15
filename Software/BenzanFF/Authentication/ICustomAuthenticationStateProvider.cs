using BenzanFF.Data.Response;
using Microsoft.AspNetCore.Components.Authorization;

namespace BenzanFF.Authentication;

public interface ICustomAuthenticationStateProvider
{
    Task<AuthenticationState> GetAuthenticationStateAsync();
    Task UpdateAuthenticationState(LoginResponse userData);
}