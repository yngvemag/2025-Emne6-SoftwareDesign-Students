using System.Net.Http.Headers;
using Microsoft.VisualBasic;

namespace StudentBloggClient.Services.Auth;

public class BasicAuthHandler(IBasicAuthStore store) : DelegatingHandler
{
    private readonly IBasicAuthStore _authStore = store;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken ct)
    {
        var authParam = await _authStore.GetAuthParametersAsync();
        if (!string.IsNullOrEmpty(authParam))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", authParam);
        }
        
        return await base.SendAsync(request, ct);
    }
}