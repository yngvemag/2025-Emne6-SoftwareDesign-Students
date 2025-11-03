using System.Net.Http.Json;
using StudentBloggClient.Models.Users;
using StudentBloggClient.Services.Auth;

namespace StudentBloggClient.Services.Api;

public class UsersApiClient(HttpClient client, IBasicAuthStore authStore): IUsersApiClient
{
    // Vi må ha en http client !!
    // IBasicAuthStore -> ha noe som kan lagre/lese authentication fra local storage/session storage
    private readonly HttpClient _httpClient = client;
    private readonly IBasicAuthStore _authStore = authStore;
    
    public async Task<IReadOnlyList<UserDto>> GetUsersAsync(int pageNumber = 1, int pageSize = 10, CancellationToken ct = default)
    {
        // https://localhost:7278/api/v1/Users?pageNumber=1&pageSize=10
        var url = $"api/v1/users?pageNumber={pageNumber}&pageSize={pageSize}";
        using var req = new HttpRequestMessage(HttpMethod.Get, url);
        
        // sender http request
        using var res = await _httpClient.SendAsync(req, ct);

        var result = await res.Content.ReadFromJsonAsync<List<UserDto>>(ct);
        
        return result ?? [];
    }

    public async Task<UserDto?> GetUserByIdAsync(Guid id, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task<UserDto?> RegisterUserAsync(UserRegistrationDto registrationDto, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task<UserDto?> UpdateUserAsync(Guid id, UserDto userDto, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task<UserDto?> DeleteUserAsync(Guid id, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}