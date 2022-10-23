using System.Net;
using ATI.Services.Common.Caching.Redis;
using ATI.Services.Common.Extensions;
using ATI.Services.Common.Initializers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Newtonsoft.Json.Serialization;
using ThousandWords.WebApi.Extensions;
using ConfigurationManager = ATI.Services.Common.Behaviors.ConfigurationManager;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager.ConfigurationRoot = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables()
    .Build();

builder.Services.AddOptions();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => options.LoginPath = PathString.Empty);
builder.Services.AddAuthorization();

builder.Services.AddControllers(opts =>
    {
        opts.SuppressInputFormatterBuffering = true;
        opts.SuppressOutputFormatterBuffering = true;
    })
    .AddNewtonsoftJson(
        options =>
        {
            options.SerializerSettings.ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };
        });


builder.Services.AddRedis();

builder.Services.AddDbContexts();
builder.Services.AddLanguageDictionaryInitializer();
builder.Services.AddApplicationServices();
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ConfigureHttpsDefaults(listenOptions => { });
    serverOptions.Listen(IPAddress.Any, ConfigurationManager.GetApplicationPort());
    serverOptions.AllowSynchronousIO = true;
});
builder.Services.AddInitializers();

var app = builder.Build();

app.MapControllers();

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(
    endpoints => { endpoints.MapControllers(); });

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    await serviceProvider.GetRequiredService<StartupInitializer>().InitializeAsync();
}

app.Run();