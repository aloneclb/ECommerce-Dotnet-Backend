namespace ETicaret.Application.Abstractions.HubServices;

public interface IProductHubService
{
    Task ProductAddedMessageAsync(string message);
}