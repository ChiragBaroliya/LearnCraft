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

        var courseLessons = await _lessonRepository.FindAsync(
            l => l.CourseId == lesson.CourseId, 
            cancellationToken);

        var sortedLessons = courseLessons
            .OrderBy(l => l.SequenceNumber)
            .ToList();

        var currentIndex = sortedLessons.FindIndex(l => l.Id == lesson.Id);
        
        Guid? previousLessonId = currentIndex > 0 
            ? sortedLessons[currentIndex - 1].Id 
            : null;
            
        Guid? nextLessonId = currentIndex < sortedLessons.Count - 1 
            ? sortedLessons[currentIndex + 1].Id 
            : null;

        return new LessonResponse(
            lesson.Id,
            lesson.Title,
            lesson.ContentUrl,
            lesson.ContentType.ToString(),
            lesson.SequenceNumber,
            lesson.CourseId,
            nextLessonId,
            previousLessonId);
    }
}
