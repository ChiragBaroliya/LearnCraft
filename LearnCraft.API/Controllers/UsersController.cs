using LearnCraft.Application.Common.Models;
using LearnCraft.Application.Features.Users.Commands.RegisterUser;
using LearnCraft.Application.Features.Users.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LearnCraft.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class UsersController : ControllerBase
{
    private readonly ISender _sender;

    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserQuery query, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return Unauthorized(ResponseDto<string>.Failure(result.Error.Message, StatusCodes.Status401Unauthorized));
        }

        return Ok(ResponseDto<string>.Success(result.Value));
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(ResponseDto<Guid>.Failure(result.Error.Message));
        }

        return Ok(ResponseDto<Guid>.Success(result.Value, "User registered successfully"));
    }
}
