using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MooDeng.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddTransient<CookieHandler>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddHttpClient("MooDengApi", client =>
    client.BaseAddress = new Uri("https://localhost:7120"))
    //.AddHttpMessageHandler<CookieHandler>()
    ;

// Add services to the container.
builder.Services.AddHttpClient();

await builder.Build().RunAsync();
