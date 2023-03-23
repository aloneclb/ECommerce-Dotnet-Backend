using ETicaret.Application.Abstractions.Services;
using ETicaret.Application.Features.User.Requests;
using ETicaret.Application.Features.User.Responses;
using ETicaret.Domain.Entities.Identity;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ETicaret.Application.Features.User.Commands;

public class UserCreateCommandHandler : IRequestHandler<UserCreateCommandRequest, UserCreateCommandResponse>
{
    private readonly IUserService _userService;
    private readonly ILogger<UserCreateCommandHandler> _logger;

    public UserCreateCommandHandler(IUserService userService, ILogger<UserCreateCommandHandler> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    public async Task<UserCreateCommandResponse> Handle(UserCreateCommandRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Kullanıcı Oluşturulmaya başlandı");
        var result = await _userService.CreateAsync(new AppUser()
        {
            Email = request.Email,
            NameSurname = request.NameSurname,
            UserName = Guid.NewGuid().ToString(),
        }, request.Password!);

        var response = new UserCreateCommandResponse()
        {
            Success = result,
            Message = result ? "Başarılı" : "Başarısız"
        };
        
        return response;
    }
}