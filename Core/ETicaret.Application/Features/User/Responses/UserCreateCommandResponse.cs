namespace ETicaret.Application.Features.User.Responses;

public class UserCreateCommandResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}