using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace ETicaret.Application;

public static class ApplicationRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services.FluentValidation();
    }
    
    private static IServiceCollection FluentValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation(options => { options.DisableDataAnnotationsValidation = true; })
            .AddFluentValidationClientsideAdapters();

        // application katmanindaki tum validator'lari tarayip, ekleyecek
        services.AddValidatorsFromAssembly(typeof(ApplicationRegistration).Assembly);

        return services;
    }
    
}