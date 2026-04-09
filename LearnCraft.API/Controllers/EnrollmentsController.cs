using LearnCraft.Application.Common.Models;
using LearnCraft.Application.Features.Enrollments.Commands.DeleteEnrollment;
using LearnCraft.Application.Features.Enrollments.Commands.EnrollUser;
using LearnCraft.Application.Features.Enrollments.Queries.GetEnrollmentById;
using LearnCraft.Application.Features.Enrollments.Queries.GetEnrollments;
using LearnCraft.Application.Features.Enrollments.Queries.GetMyEnrollments;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetEnrollments(
        [FromQuery] int pageNumber = 1, 
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new GetEnrollmentsQuery(pageNumber, pageSize), cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(ResponseDto<PagedResult<EnrollmentResponse>>.Failure(result.Error.Message));
        }

        return Ok(ResponseDto<PagedResult<EnrollmentResponse>>.Success(result.Value));
    }

    [HttpGet("my")]
    [Authorize]
    public async Task<IActionResult> GetMyEnrollments(CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) 
        {
            return Unauthorized(ResponseDto<object>.Failure("User ID not found in token.", StatusCodes.Status401Unauthorized));
        }

        var result = await _sender.Send(new GetMyEnrollmentsQuery(Guid.Parse(userId)), cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(ResponseDto<List<MyEnrollmentResponse>>.Failure(result.Error.Message));
        }

        return Ok(ResponseDto<List<MyEnrollmentResponse>>.Success(result.Value));
    }

    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> GetEnrollmentById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetEnrollmentByIdQuery(id), cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.Code == "Enrollment.NotFound" 
                ? NotFound(ResponseDto<EnrollmentResponse>.Failure(result.Error.Message, StatusCodes.Status404NotFound))
                : BadRequest(ResponseDto<EnrollmentResponse>.Failure(result.Error.Message));
        }

        return Ok(ResponseDto<EnrollmentResponse>.Success(result.Value));
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

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Instructor,Admin")]
    public async Task<IActionResult> DeleteEnrollment(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new DeleteEnrollmentCommand(id), cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.Code == "Enrollment.NotFound"
                ? NotFound(ResponseDto<Guid>.Failure(result.Error.Message, StatusCodes.Status404NotFound))
                : BadRequest(ResponseDto<Guid>.Failure(result.Error.Message));
        }

        return Ok(ResponseDto<Guid>.Success(id, "Enrollment deleted successfully"));
    }
}
