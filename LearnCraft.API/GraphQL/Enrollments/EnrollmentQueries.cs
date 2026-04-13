using LearnCraft.Application.Common.Models;
using LearnCraft.Application.Features.Enrollments.Queries.GetEnrollmentById;
using LearnCraft.Application.Features.Enrollments.Queries.GetEnrollments;
using LearnCraft.Application.Features.Enrollments.Queries.GetMyEnrollments;
using LearnCraft.Application.Features.Courses.Queries.GetCourses;
using MediatR;
using System.Security.Claims;
using HotChocolate.Authorization;

namespace LearnCraft.API.GraphQL.Enrollments;

[ExtendObjectType("Query")]
public sealed class EnrollmentQueries
{
    [Authorize]
    public async Task<PagedResult<EnrollmentResponse>> GetEnrollments(
        [Service] ISender sender,
        int pageNumber = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetEnrollmentsQuery(pageNumber, pageSize), cancellationToken);

        if (result.IsFailure)
        {
            throw new Exception(result.Error.Message);
        }

        return result.Value;
    }

    [Authorize]
    public async Task<List<CourseResponse>> GetMyEnrollments(
        [Service] ISender sender,
        ClaimsPrincipal claimsPrincipal,
        CancellationToken cancellationToken)
    {
        var userIdString = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
        {
            throw new Exception("User ID not found in token.");
        }

        var result = await sender.Send(new GetMyEnrollmentsQuery(userId), cancellationToken);

        if (result.IsFailure)
        {
            throw new Exception(result.Error.Message);
        }

        return result.Value;
    }

    [Authorize]
    public async Task<EnrollmentResponse> GetEnrollmentById(
        [Service] ISender sender,
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetEnrollmentByIdQuery(id), cancellationToken);

        if (result.IsFailure)
        {
            throw new Exception(result.Error.Message);
        }

        return result.Value;
    }
}
