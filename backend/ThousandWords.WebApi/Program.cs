using ATI.Services.Common.Caching.Redis;
using ATI.Services.Common.Initializers;
using Microsoft.AspNetCore.Authentication.Cookies;
using ThousandWords.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);
ATI.Services.Common.Behaviors.ConfigurationManager.ConfigurationRoot = builder.Configuration;

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => options.LoginPath = PathString.Empty);
builder.Services.AddAuthorization();

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddRedis();

builder.Services.AddDbContexts();
builder.Services.AddLanguageDictionaryInitializer();
builder.Services.AddApplicationServices();

var app = builder.Build();

app.MapControllers();

var atiInitializer = new StartupInitializer(app.Services);
await atiInitializer.InitializeAsync();

app.UseAuthentication();
app.UseAuthorization();

app.Run();