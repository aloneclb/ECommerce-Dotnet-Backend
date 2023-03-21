using ETicaret.Application.Features.Auth.Requests;
using ETicaret.Application.Features.User.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.API.Controllers.User;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = "Admin")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("create/")]
    public async Task<IActionResult> Create(UserCreateCommandRequest request)
    {
        var result = await _mediator.Send(request);
        return result.Success
            ? Ok(result.Message)
            : BadRequest(result.Message);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("login/")]
    public async Task<IActionResult> Login(UserLoginCommandRequest request)
    {
        var result = await _mediator.Send(request);
        return Ok(result.Token);
    }

    [HttpPost]
    [Route("refresh/")]
    [AllowAnonymous]
    public async Task<IActionResult> Refresh(RefreshTokenCommandRequest input)
    {
        var result = await _mediator.Send(input);
        return Ok(result);
    }
}