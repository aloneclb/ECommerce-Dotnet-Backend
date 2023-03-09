using ETicaret.Application.Services;
using Eticaret.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Eticaret.Infrastructure;

public static class InfrastructureRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IFileService, FileService>();
    }
}