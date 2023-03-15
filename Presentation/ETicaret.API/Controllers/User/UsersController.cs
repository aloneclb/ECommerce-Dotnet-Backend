using ETicaret.Application.Features.User.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.API.Controllers.User;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
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
}