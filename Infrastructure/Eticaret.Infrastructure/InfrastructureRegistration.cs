using ETicaret.Application.Abstractions.Storage;
using ETicaret.Application.Abstractions.Token;
using Eticaret.Infrastructure.Services.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Eticaret.Infrastructure;

public static class InfrastructureRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenHandler, Services.Token.TokenHandler>();
        
        // services.AddScoped<IFileService, FileService>();
        services.AddScoped<IStorageService, StorageService>();
    }

    public static void AddStorage<T>(this IServiceCollection services) where T : class, IStorage
    {
        services.AddScoped<IStorage, T>();
    }
}