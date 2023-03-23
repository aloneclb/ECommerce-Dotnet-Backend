﻿using ETicaret.API.Middlewares;

namespace ETicaret.API.Extensions;

public static class ConfigureGlobalExceptionHandlerExtension
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
    }

    // public static void UseGlobalExceptionHandler<T>(this WebApplication application, ILogger<T> logger)
    // {
    //     application.UseExceptionHandler(builder =>
    //     {
    //         builder.Run(async context =>
    //         {
    //             context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    //             context.Response.ContentType = MediaTypeNames.Application.Json;
    //
    //             var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
    //             if (contextFeature != null)
    //             {
    //                 logger.LogError(contextFeature.Error.Message);
    //
    //                 await context.Response.WriteAsync(JsonSerializer.Serialize(new
    //                 {
    //                     StatusCode = context.Response.StatusCode,
    //                     Message = contextFeature.Error.Message,
    //                     Title = "Hata alındı!"
    //                 }));
    //                 ;
    //             }
    //         });
    //     });
    // }
}