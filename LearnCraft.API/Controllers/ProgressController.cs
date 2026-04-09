using LearnCraft.Application.Common.Models;
using LearnCraft.Application.Features.Progress.Commands.CompleteLesson;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnCraft.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ProgressController : ControllerBase
{
    private readonly ISender _sender;

    public ProgressController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("complete-lesson")]
    [Authorize]
    public async Task<IActionResult> CompleteLesson([FromBody] CompleteLessonCommand command, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(ResponseDto<object>.Failure(result.Error.Message));
        }

        return Ok(ResponseDto<object>.Success(null, "Lesson marked as completed"));
    }
}
