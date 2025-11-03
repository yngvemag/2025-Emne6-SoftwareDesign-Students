using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using StudentBloggClient;
using StudentBloggClient.Services.Api;
using StudentBloggClient.Services.Auth;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IBasicAuthStore, BasicAuthStore>();

builder.Services.AddHttpClient<IUsersApiClient, UsersApiClient>()
    .ConfigureHttpClient((sp, client) =>
    {
        // addres to 
        client.BaseAddress = new Uri("https://localhost:7278");
    });

await builder.Build().RunAsync();
