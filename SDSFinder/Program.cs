using DevExpress.Blazor;

using Indium.Common.EFContexts;
using Indium.Common.Modules;
using Indium.Infor.EFContexts;

using Indium.Common.Modules.Auth;
using Microsoft.AspNetCore.Authorization;
using MudBlazor.Services;
using SDSFinder.EFContexts;
using SDSFinder.Modules.Repositories;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Check out the differences between Mvc, ControllersWithViews, Controller, and RazorPages:
// https://dotnettutorials.net/wp-content/uploads/2019/03/word-image-16-1024x302.png
builder.Services.AddRazorPages();

// Services Configuration
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMudServices();
builder.Services.AddDevExpressBlazor();

// Specify DevExpress Bootstrap
builder.Services.Configure<DevExpress.Blazor.Configuration.GlobalOptions>(options =>
{
    options.BootstrapVersion = BootstrapVersion.v5;
});

// Indium services and repositories
builder.Services.AddScoped<TimeZoneService>();

builder.Services.AddScoped<ISiteRepository, SiteRepository>();
builder.Services.AddScoped<IJobRepository, JobRepository>();

// Authentication and Authorization services
builder.Services.AddScoped<IAuthorizationHandler, ActiveDirectoryAuthorizationHandler>();
builder.Services.AddScoped<IPermissionService, ActiveDirectoryPermissionService>();

// Appseting AD Groups for Authorization
builder.Services.AddAuthorization(options =>
{
    IEnumerable<IConfigurationSection> policies = builder.Configuration.GetSection("AuthorizationPolicyGroups").GetChildren();
    foreach (IConfigurationSection policy in policies)
    {
        if (policy.Value is not null)
        {
            string[] adGroups = policy.Value.Split(",").Select(x => x.Trim()).ToArray();
            options.AddPolicy(policy.Key, x =>
                x.RequireAuthenticatedUser().RequireRole(adGroups)
            );
        }
    }
});

// Add database connections
builder.Services.AddDbContextFactory<SDSFinderContext>(options =>
{
    string? connectionString = builder.Configuration.GetConnectionString("app");
    if (connectionString is not null)
    {
        options.UseSqlServer(connectionString);
    }

    options.EnableSensitiveDataLogging(false);
}, ServiceLifetime.Transient);

builder.Services.AddDbContextFactory<CommonContext>(options =>
{
    string? connectionString = builder.Configuration.GetConnectionString("common");
    if (connectionString is not null)
    {
        options.UseSqlServer(connectionString);
    }

    options.EnableSensitiveDataLogging(false);
}, ServiceLifetime.Transient);

// Logging configuration
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddEventLog();
builder.Logging.SetMinimumLevel(LogLevel.Debug);

// Web Host configuration
builder.WebHost.UseWebRoot("wwwroot");
builder.WebHost.UseStaticWebAssets();

// App Build configuration
WebApplication app = builder.Build();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();

//app.MapControllers();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();