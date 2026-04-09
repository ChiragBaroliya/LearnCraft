using LearnCraft.Application.Interfaces.Repositories;
using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Lessons.Queries.GetLessonById;

public sealed class GetLessonByIdQueryHandler 
    : IRequestHandler<GetLessonByIdQuery, Result<LessonResponse>>
{
    private readonly ILessonRepository _lessonRepository;

    public GetLessonByIdQueryHandler(ILessonRepository lessonRepository)
    {
        _lessonRepository = lessonRepository;
    }

    public async Task<Result<LessonResponse>> Handle(
        GetLessonByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var lesson = await _lessonRepository.GetByIdAsync(request.Id, cancellationToken);

        if (lesson is null)
        {
            return Result.Failure<LessonResponse>(new Error("Lesson.NotFound", "Lesson not found"));
        }

        return new LessonResponse(
            lesson.Id,
            lesson.Title,
            lesson.ContentUrl,
            lesson.ContentType.ToString(),
            lesson.SequenceNumber,
            lesson.CourseId);
    }
}
