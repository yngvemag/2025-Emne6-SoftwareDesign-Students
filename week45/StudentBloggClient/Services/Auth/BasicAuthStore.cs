using System.Text;
using System.Text.Json;
using Microsoft.JSInterop;

namespace StudentBloggClient.Services.Auth;

public class BasicAuthStore(IJSRuntime js)  : IBasicAuthStore
{
    private readonly IJSRuntime _js = js;
    private string? _username;
    private string? _authParam;
    private bool _loaded;
    
    public string? UserName => _username;
    public bool IsAuthenticated => !string.IsNullOrEmpty(_authParam);
    
    private const string StorageKey = "auth.basic";
    
    public event Action? Changed;
    
    public async Task<string?> GetAuthParametersAsync()
    {
        if (!_loaded)
        {
            await LoadAsync();
        }
        return _authParam;
    }

    public async Task SetAsync(string username, string password, bool rememberMe, CancellationToken ct = default)
    {
        _username = username;
        // "username:password" -> base64encoding
        var rawData = Encoding.UTF8.GetBytes($"{username}:{password}");
        _authParam = Convert.ToBase64String(rawData);

        if (rememberMe)
        {
            var payload = JsonSerializer.Serialize(new PersistedAuth
            {
                 UserName = username,
                 Password = password,
                 Base64Encoded = _authParam
             });
            await _js.InvokeVoidAsync("sessionStorage.setItem", StorageKey, payload);
        }
        else
        {
            await _js.InvokeVoidAsync("sessionStorage.removeItem", StorageKey);
        }

        _loaded = true;
        Changed?.Invoke();
    }

    public async Task ClearAsync(CancellationToken ct = default)
    {
        _username = null;
        _authParam = null;
        _loaded = false;
        await _js.InvokeVoidAsync("sessionStorage.removeItem", StorageKey);
        Changed?.Invoke();
    }

    public async Task LoadAsync()
    {
        try
        {
            string json = await _js.InvokeAsync<string>("sessionStorage.getItem", StorageKey);            
            var persistedAuth = JsonSerializer.Deserialize<PersistedAuth>(json);
            _username = persistedAuth?.UserName;            
            //var rawData = Encoding.UTF8.GetBytes($"{_username}:{persistedAuth?.Password}");
            _authParam =  persistedAuth?.Base64Encoded;//  Convert.ToBase64String(rawData);
        }
        catch (Exception) {}
        finally
        {
            _loaded = true;
            // Notify listeners (e.g., NavMenu) that auth state may have changed after a full refresh
            Changed?.Invoke();
        }
    }
  
    private class PersistedAuth
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Base64Encoded {get; set;}
    }
}