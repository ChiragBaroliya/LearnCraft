using LearnCraft.Application.Common.Models;
using LearnCraft.Application.Features.Courses.Commands.CreateCourse;
using LearnCraft.Application.Features.Courses.Queries.GetCourses;
using LearnCraft.Domain.Primitives;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnCraft.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class CoursesController : ControllerBase
{
    private readonly ISender _sender;

    public CoursesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetCourses(CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetCoursesQuery(), cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(ResponseDto<List<CourseResponse>>.Failure(result.Error.Message));
        }

        return Ok(ResponseDto<List<CourseResponse>>.Success(result.Value));
    }

    [HttpPost]
    [Authorize(Roles = "Instructor,Admin")]
    public async Task<IActionResult> CreateCourse(
        [FromBody] CreateCourseCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(ResponseDto<Guid>.Failure(result.Error.Message));
        }

        return CreatedAtAction(
            nameof(GetCourses), 
            new { id = result.Value }, 
            ResponseDto<Guid>.Success(result.Value, "Course created successfully", StatusCodes.Status201Created));
    }
}
