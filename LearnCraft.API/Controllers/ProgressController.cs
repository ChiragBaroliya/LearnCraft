using LearnCraft.Application.Common.Models;
using LearnCraft.Application.Features.Progress.Commands.CompleteLesson;
using LearnCraft.Application.Features.Progress.Commands.DeleteProgress;
using LearnCraft.Application.Features.Progress.Queries.GetProgress;
using LearnCraft.Application.Features.Progress.Queries.GetProgressById;
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

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetProgress(
        [FromQuery] int pageNumber = 1, 
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new GetProgressQuery(pageNumber, pageSize), cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(ResponseDto<PagedResult<ProgressResponse>>.Failure(result.Error.Message));
        }

        return Ok(ResponseDto<PagedResult<ProgressResponse>>.Success(result.Value));
    }

    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> GetProgressById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetProgressByIdQuery(id), cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.Code == "Progress.NotFound" 
                ? NotFound(ResponseDto<ProgressResponse>.Failure(result.Error.Message, StatusCodes.Status404NotFound))
                : BadRequest(ResponseDto<ProgressResponse>.Failure(result.Error.Message));
        }

        return Ok(ResponseDto<ProgressResponse>.Success(result.Value));
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

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Instructor,Admin")]
    public async Task<IActionResult> DeleteProgress(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new DeleteProgressCommand(id), cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.Code == "Progress.NotFound"
                ? NotFound(ResponseDto<Guid>.Failure(result.Error.Message, StatusCodes.Status404NotFound))
                : BadRequest(ResponseDto<Guid>.Failure(result.Error.Message));
        }

        return Ok(ResponseDto<Guid>.Success(id, "Progress deleted successfully"));
    }
}
