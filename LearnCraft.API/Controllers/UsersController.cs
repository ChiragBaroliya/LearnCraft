using LearnCraft.Application.Common.Models;
using LearnCraft.Application.Features.Users.Commands.DeleteUser;
using LearnCraft.Application.Features.Users.Commands.RegisterUser;
using LearnCraft.Application.Features.Users.Queries.GetUserById;
using LearnCraft.Application.Features.Users.Queries.GetUsers;
using LearnCraft.Application.Features.Users.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUsers(
        [FromQuery] int pageNumber = 1, 
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new GetUsersQuery(pageNumber, pageSize), cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(ResponseDto<PagedResult<UserResponse>>.Failure(result.Error.Message));
        }

        return Ok(ResponseDto<PagedResult<UserResponse>>.Success(result.Value));
    }

    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetUserByIdQuery(id), cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.Code == "User.NotFound" 
                ? NotFound(ResponseDto<UserResponse>.Failure(result.Error.Message, StatusCodes.Status404NotFound))
                : BadRequest(ResponseDto<UserResponse>.Failure(result.Error.Message));
        }

        return Ok(ResponseDto<UserResponse>.Success(result.Value));
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

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new DeleteUserCommand(id), cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.Code == "User.NotFound"
                ? NotFound(ResponseDto<Guid>.Failure(result.Error.Message, StatusCodes.Status404NotFound))
                : BadRequest(ResponseDto<Guid>.Failure(result.Error.Message));
        }

        return Ok(ResponseDto<Guid>.Success(id, "User deleted successfully"));
    }
}
