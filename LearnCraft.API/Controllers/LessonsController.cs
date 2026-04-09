using LearnCraft.Application.Common.Models;
using LearnCraft.Application.Features.Lessons.Commands.AddLesson;
using LearnCraft.Application.Features.Lessons.Commands.DeleteLesson;
using LearnCraft.Application.Features.Lessons.Queries.GetLessonById;
using LearnCraft.Application.Features.Lessons.Queries.GetLessons;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnCraft.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class LessonsController : ControllerBase
{
    private readonly ISender _sender;

    public LessonsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetLessons(
        [FromQuery] int pageNumber = 1, 
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new GetLessonsQuery(pageNumber, pageSize), cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(ResponseDto<PagedResult<LessonResponse>>.Failure(result.Error.Message));
        }

        return Ok(ResponseDto<PagedResult<LessonResponse>>.Success(result.Value));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetLessonById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetLessonByIdQuery(id), cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.Code == "Lesson.NotFound" 
                ? NotFound(ResponseDto<LessonResponse>.Failure(result.Error.Message, StatusCodes.Status404NotFound))
                : BadRequest(ResponseDto<LessonResponse>.Failure(result.Error.Message));
        }

        return Ok(ResponseDto<LessonResponse>.Success(result.Value));
    }

    [HttpPost]
    [Authorize(Roles = "Instructor,Admin")]
    public async Task<IActionResult> AddLesson([FromBody] AddLessonCommand command, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(ResponseDto<Guid>.Failure(result.Error.Message));
        }

        return Ok(ResponseDto<Guid>.Success(result.Value, "Lesson added successfully"));
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Instructor,Admin")]
    public async Task<IActionResult> DeleteLesson(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new DeleteLessonCommand(id), cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.Code == "Lesson.NotFound"
                ? NotFound(ResponseDto<Guid>.Failure(result.Error.Message, StatusCodes.Status404NotFound))
                : BadRequest(ResponseDto<Guid>.Failure(result.Error.Message));
        }

        return Ok(ResponseDto<Guid>.Success(id, "Lesson deleted successfully"));
    }
}
