using ETicaret.Application.Abstractions.HubServices;
using Eticaret.WebSocket.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Eticaret.WebSocket.HubServices;

public class ProductHubService : IProductHubService
{
    private readonly IHubContext<ProductHub> _hubContext;

    public ProductHubService(IHubContext<ProductHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task ProductAddedMessageAsync(string message)
    {
        await _hubContext.Clients.All.SendAsync("receiveProductAddedMessage", message);
    }
}