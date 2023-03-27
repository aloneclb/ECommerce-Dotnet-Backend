using ETicaret.Application.Abstractions.HubServices;
using Eticaret.WebSocket.HubServices;
using Microsoft.Extensions.DependencyInjection;

namespace Eticaret.WebSocket;

public static class ServiceRegistration
{
    public static void AddWebSocketServices(this IServiceCollection services)
    {
        services.AddTransient<IProductHubService, ProductHubService>();
        services.AddSignalR();
    }
}