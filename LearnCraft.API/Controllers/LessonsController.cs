using LearnCraft.Application.Common.Models;
using LearnCraft.Application.Features.Lessons.Commands.AddLesson;
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
}
