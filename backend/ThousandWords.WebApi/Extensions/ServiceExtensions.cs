using ATI.Services.Common.Extensions;
using ThousandWords.Core.Initializers;
using ThousandWords.Core.Interfaces.DbContexts;
using ThousandWords.Core.Interfaces.ExternalLoaders;
using ThousandWords.Core.Services.CompleteWord;
using ThousandWords.Core.Services.GetWords;
using ThousandWords.FileAccess;
using ThousandWords.RedisAccess;
using ThousandWords.WebApi.Services;

namespace ThousandWords.WebApi.Extensions;

public static class ServiceExtensions
{
    public static void AddDbContexts(this IServiceCollection services)
    {
        services.AddSingleton<ILanguageDictionaryInfoDbContext, LanguageDictionaryInfoDbContext>();
        services.AddSingleton<ILanguagePairsDbContext, LanguagePairsDbContext>();
        services.AddSingleton<IUsersDbContext, UsersDbContext>();
    }

    public static void AddLanguageDictionaryInitializer(this IServiceCollection services)
    {
        services.ConfigureByName<FilesManagerOptions>();
        services.AddTransient<ILanguageDictionariesLoader, CsvFilesLoader>();
        services.AddTransient<LanguageDictionariesInitializer>();
    }
    
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<AuthService>();
        services.AddSingleton<GetWordsService>();
        services.AddSingleton<CompleteWordService>();
    }
}