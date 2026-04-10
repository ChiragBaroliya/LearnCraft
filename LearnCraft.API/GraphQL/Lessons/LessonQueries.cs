using LearnCraft.Application.Common.Models;
using LearnCraft.Application.Features.Lessons.Queries.GetLessonById;
using LearnCraft.Application.Features.Lessons.Queries.GetLessons;
using MediatR;

namespace LearnCraft.API.GraphQL.Lessons;

[ExtendObjectType("Query")]
public sealed class LessonQueries
{
    public async Task<PagedResult<LessonResponse>> GetLessons(
        [Service] ISender sender,
        int pageNumber = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetLessonsQuery(pageNumber, pageSize), cancellationToken);

        if (result.IsFailure)
        {
            throw new Exception(result.Error.Message);
        }

        return result.Value;
    }

    public async Task<LessonResponse> GetLessonById(
        [Service] ISender sender,
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetLessonByIdQuery(id), cancellationToken);

        if (result.IsFailure)
        {
            throw new Exception(result.Error.Message);
        }

        return result.Value;
    }
}
