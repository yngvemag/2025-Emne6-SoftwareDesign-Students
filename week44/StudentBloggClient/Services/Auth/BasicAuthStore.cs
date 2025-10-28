namespace StudentBloggClient.Services.Auth;

public class BasicAuthStore : IBasicAuthStore
{
    public string? UserName { get; }
    public bool IsAuthenticated { get; }
    public async Task<string?> GetAuthParametersAsync()
    {
        throw new NotImplementedException();
    }

    public async Task SetAsync(string username, string password, bool rememberMe, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task ClearAsync(CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public event Action? Changed;
}