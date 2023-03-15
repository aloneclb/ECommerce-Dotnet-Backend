using ETicaret.Application.Exceptions;
using ETicaret.Application.Features.User.Requests;
using ETicaret.Application.Features.User.Responses;
using ETicaret.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETicaret.Application.Features.User.Commands;

public class UserCreateCommandHandler : IRequestHandler<UserCreateCommandRequest, UserCreateCommandResponse>
{
    private readonly UserManager<AppUser> _userManager;

    public UserCreateCommandHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<UserCreateCommandResponse> Handle(UserCreateCommandRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _userManager.CreateAsync(new AppUser()
        {
            Email = request.Email,
            NameSurname = request.NameSurname,
            UserName = Guid.NewGuid().ToString(),
        }, request.Password!);

        var response = new UserCreateCommandResponse()
        {
            Success = result.Succeeded,
        };
        if (result.Succeeded)
            response.Message = "Başarılı";
        else
            response.Message = "Başarısız";

        return response;
    }
}