using Blazored.Modal;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebApp;
using WebApp.Auth;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazoredToast();

builder.Services.AddBlazoredModal();

builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<JWTAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, JWTAuthenticationStateProvider>(
    provider => provider.GetRequiredService<JWTAuthenticationStateProvider>()
);

builder.Services.AddScoped<ILoginService, JWTAuthenticationStateProvider>(
    provider => provider.GetRequiredService<JWTAuthenticationStateProvider>()
);

await builder.Build().RunAsync();