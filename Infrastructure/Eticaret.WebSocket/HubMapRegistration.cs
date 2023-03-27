using Eticaret.WebSocket.Hubs;
using Microsoft.AspNetCore.Builder;

namespace Eticaret.WebSocket;

public static class HubMapRegistration
{
    public static void MapHubs(this WebApplication webApplication)
    {
        webApplication.MapHub<ProductHub>("/product-hub");
    }
}