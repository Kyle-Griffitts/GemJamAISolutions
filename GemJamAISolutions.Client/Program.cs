using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using GemJamAISolutions.Client.Services;
using dymaptic.GeoBlazor.Core;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<AuthService>();
builder.Services.AddGeoBlazor(builder.Configuration);

await builder.Build().RunAsync();
