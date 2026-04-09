using LearnCraft.Application.Common.Models;
using LearnCraft.Application.Features.Courses.Commands.CreateCourse;
using LearnCraft.Application.Features.Courses.Commands.DeleteCourse;
using LearnCraft.Application.Features.Courses.Queries.GetCourseById;
using LearnCraft.Application.Features.Courses.Queries.GetCourseLessons;
using LearnCraft.Application.Features.Courses.Queries.GetCourses;
using LearnCraft.Application.Features.Lessons.Queries.GetLessonById;
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
    public async Task<IActionResult> GetCourses(
        [FromQuery] int pageNumber = 1, 
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new GetCoursesQuery(pageNumber, pageSize), cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(ResponseDto<PagedResult<CourseResponse>>.Failure(result.Error.Message));
        }

        return Ok(ResponseDto<PagedResult<CourseResponse>>.Success(result.Value));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCourseById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetCourseByIdQuery(id), cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.Code == "Course.NotFound" 
                ? NotFound(ResponseDto<CourseDetailsResponse>.Failure(result.Error.Message, StatusCodes.Status404NotFound))
                : BadRequest(ResponseDto<CourseDetailsResponse>.Failure(result.Error.Message));
        }

        return Ok(ResponseDto<CourseDetailsResponse>.Success(result.Value));
    }

    [HttpGet("{courseId:guid}/lessons")]
    public async Task<IActionResult> GetCourseLessons(Guid courseId, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetCourseLessonsQuery(courseId), cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(ResponseDto<List<LessonResponse>>.Failure(result.Error.Message));
        }

        return Ok(ResponseDto<List<LessonResponse>>.Success(result.Value));
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

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Instructor,Admin")]
    public async Task<IActionResult> DeleteCourse(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new DeleteCourseCommand(id), cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.Code == "Course.NotFound"
                ? NotFound(ResponseDto<Guid>.Failure(result.Error.Message, StatusCodes.Status404NotFound))
                : BadRequest(ResponseDto<Guid>.Failure(result.Error.Message));
        }

        return Ok(ResponseDto<Guid>.Success(id, "Course deleted successfully"));
    }
}
