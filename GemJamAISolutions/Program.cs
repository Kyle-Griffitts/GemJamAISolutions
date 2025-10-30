using GemJamAISolutions.Client.Pages;
using GemJamAISolutions.Components;
using GemJamAISolutions.Data;
using GemJamAISolutions.Endpoints;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using dymaptic.GeoBlazor.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;

    // User settings
    options.User.RequireUniqueEmail = true;

    // Sign-in settings
    options.SignIn.RequireConfirmedEmail = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(7);
    options.LoginPath = "/auth";
    options.AccessDeniedPath = "/access-denied";
    options.SlidingExpiration = true;
});

builder.Services.AddHttpClient();
builder.Services.AddScoped(sp =>
{
    var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
    var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient();

    // Get the base URL from the current request
    var request = httpContextAccessor.HttpContext?.Request;
    if (request != null)
    {
        var baseUrl = $"{request.Scheme}://{request.Host}";
        httpClient.BaseAddress = new Uri(baseUrl);
    }

    return new GemJamAISolutions.Client.Services.AuthService(httpClient);
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddGeoBlazor(builder.Configuration);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(GemJamAISolutions.Client._Imports).Assembly);

app.MapAuthEndpoints();

app.Run();
