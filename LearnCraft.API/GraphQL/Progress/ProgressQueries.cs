using LearnCraft.Application.Common.Models;
using LearnCraft.Application.Features.Progress.Queries.GetCourseProgress;
using LearnCraft.Application.Features.Progress.Queries.GetProgress;
using LearnCraft.Application.Features.Progress.Queries.GetProgressById;
using LearnCraft.Application.Features.Lessons.Queries.GetResumeLesson;
using LearnCraft.Application.Features.Lessons.Queries.GetLessonById;
using MediatR;
using System.Security.Claims;
using HotChocolate.Authorization;

namespace LearnCraft.API.GraphQL.Progress;

[ExtendObjectType("Query")]
public sealed class ProgressQueries
{
    [Authorize]
    public async Task<PagedResult<ProgressResponse>> GetProgress(
        [Service] ISender sender,
        int pageNumber = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetProgressQuery(pageNumber, pageSize), cancellationToken);

        if (result.IsFailure)
        {
            throw new Exception(result.Error.Message);
        }

        return result.Value;
    }

    [Authorize]
    public async Task<CourseProgressResponse> GetCourseProgress(
         [Service] ISender sender,
         ClaimsPrincipal claimsPrincipal,
        Guid courseId, CancellationToken cancellationToken)
    {
        var userIdString = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
        {
            throw new Exception("User ID not found in token.");
        }

        var result = await sender.Send(new GetCourseProgressQuery(courseId, userId), cancellationToken);

        if (result.IsFailure)
        {
            throw new Exception(result.Error.Message);
        }

        return result.Value;
    }

    [Authorize]
    public async Task<LessonResponse> GetResumeLesson(
        [Service] ISender sender,
        ClaimsPrincipal claimsPrincipal,
        Guid courseId,
        CancellationToken cancellationToken)
    {
        var userIdString = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
        {
            throw new Exception("User ID not found in token.");
        }

        var result = await sender.Send(new GetResumeLessonQuery(courseId, userId), cancellationToken);

        if (result.IsFailure)
        {
            throw new Exception(result.Error.Message);
        }

        return result.Value;
    }

    [Authorize]
    public async Task<ProgressResponse> GetProgressById(
        [Service] ISender sender, 
        Guid id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetProgressByIdQuery(id), cancellationToken);

        if (result.IsFailure)
        {
            throw new Exception(result.Error.Message);
        }

        return result.Value;
    }
}
