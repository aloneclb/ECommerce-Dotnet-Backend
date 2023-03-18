using ETicaret.Application.Abstractions.Services;
using ETicaret.Application.Exceptions;
using ETicaret.Application.Features.User.Requests;
using ETicaret.Application.Features.User.Responses;
using ETicaret.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETicaret.Application.Features.User.Commands;

public class UserCreateCommandHandler : IRequestHandler<UserCreateCommandRequest, UserCreateCommandResponse>
{
    private readonly IUserService _userService;

    public UserCreateCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<UserCreateCommandResponse> Handle(UserCreateCommandRequest request,
        CancellationToken cancellationToken)
    {
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