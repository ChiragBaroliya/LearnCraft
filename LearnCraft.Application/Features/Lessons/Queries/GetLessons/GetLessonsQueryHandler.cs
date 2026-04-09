using LearnCraft.Application.Common.Models;
using LearnCraft.Application.Features.Lessons.Queries.GetLessonById;
using LearnCraft.Application.Interfaces.Repositories;
using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Lessons.Queries.GetLessons;

public sealed class GetLessonsQueryHandler 
    : IRequestHandler<GetLessonsQuery, Result<PagedResult<LessonResponse>>>
{
    private readonly ILessonRepository _lessonRepository;

    public GetLessonsQueryHandler(ILessonRepository lessonRepository)
    {
        _lessonRepository = lessonRepository;
    }

    public async Task<Result<PagedResult<LessonResponse>>> Handle(
        GetLessonsQuery request, 
        CancellationToken cancellationToken)
    {
        var (items, totalCount) = await _lessonRepository.GetPagedAsync(
            request.PageNumber, 
            request.PageSize, 
            cancellationToken);

        var lessonResponses = items
            .Select(l => new LessonResponse(
                l.Id,
                l.Title,
                l.ContentUrl,
                l.ContentType.ToString(),
                l.SequenceNumber,
                l.CourseId))
            .ToList();

        return PagedResult<LessonResponse>.Create(
            lessonResponses, 
            totalCount, 
            request.PageNumber, 
            request.PageSize);
    }
}
