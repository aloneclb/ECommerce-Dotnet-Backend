using ETicaret.Application.Abstractions.Token;
using ETicaret.Application.Exceptions;
using ETicaret.Application.Features.User.Requests;
using ETicaret.Application.Features.User.Responses;
using ETicaret.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETicaret.Application.Features.User.Commands;

public class UserLoginCommandHandler : IRequestHandler<UserLoginCommandRequest, UserLoginCommandResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenHandler _tokenHandler;

    public UserLoginCommandHandler(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        ITokenHandler tokenHandler)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenHandler = tokenHandler;
    }

    public async Task<UserLoginCommandResponse> Handle(UserLoginCommandRequest request,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.EmailorUserName!)
                   ?? await _userManager.FindByNameAsync(request.EmailorUserName!);

        if (user is null)
            throw new NotFoundUserException();

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password!, false);
        if (!result.Succeeded)
            throw new NotFoundUserException();

        var token = _tokenHandler.CreateAccessToken(30);

        return new UserLoginCommandResponse()
        {
            Token = token
        };
    }
}