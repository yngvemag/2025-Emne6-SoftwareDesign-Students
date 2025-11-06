using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using StudentBloggClient;
using StudentBloggClient.Services.Api;
using StudentBloggClient.Services.Auth;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Use a single shared auth store so handlers and components see the same state immediately
builder.Services.AddSingleton<IBasicAuthStore, BasicAuthStore>();
//builder.Services.AddScoped<BasicAuthHandler>();
builder.Services.AddScoped<BasicAuthHandler>();

builder.Services.AddHttpClient<IUsersApiClient, UsersApiClient>()
    .ConfigureHttpClient((sp, client) =>
    {
        // addres to 
        client.BaseAddress = new Uri("https://localhost:7278");
    }).AddHttpMessageHandler<BasicAuthHandler>();

var host = builder.Build();

// Preload auth state from sessionStorage so UI reflects login on first render after refresh
var authStore = host.Services.GetRequiredService<IBasicAuthStore>();
await authStore.LoadAsync();

await host.RunAsync();
