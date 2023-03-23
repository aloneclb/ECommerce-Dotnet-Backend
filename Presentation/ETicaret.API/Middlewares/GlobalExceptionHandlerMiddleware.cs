using System.Net;
using System.Net.Mime;
using System.Text.Json;
using ETicaret.API.Controllers.Product;
using Microsoft.AspNetCore.Diagnostics;

namespace ETicaret.API.Middlewares;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ProductsController> _logger;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<ProductsController> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message.ToString());
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = MediaTypeNames.Application.Json;

            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                StatusCode = context.Response.StatusCode,
                Message = ex.Message,
                Title = "Hata alındı!"
            }));
        }
    }
}