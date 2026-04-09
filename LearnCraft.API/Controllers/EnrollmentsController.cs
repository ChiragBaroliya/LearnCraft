using LearnCraft.Application.Common.Models;
using LearnCraft.Application.Features.Enrollments.Commands.EnrollUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnCraft.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class EnrollmentsController : ControllerBase
{
    private readonly ISender _sender;

    public EnrollmentsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Enroll([FromBody] EnrollUserCommand command, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(ResponseDto<Guid>.Failure(result.Error.Message));
        }

        return Ok(ResponseDto<Guid>.Success(result.Value, "Enrolled successfully"));
    }
}
