namespace StudentBloggClient.Services.Auth;

public interface IBasicAuthStore
{
    string? UserName { get;}
    bool IsAuthenticated { get; }

    Task<string?> GetAuthParametersAsync();
    
    Task SetAsync(string username, string password, bool rememberMe, CancellationToken ct = default);
    
    Task ClearAsync(CancellationToken ct = default);

    Task LoadAsync();

    event Action? Changed;
}