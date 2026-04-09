using LearnCraft.Application.Features.Lessons.Queries.GetLessonById;
using LearnCraft.Application.Interfaces.Repositories;
using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Courses.Queries.GetCourseLessons;

public sealed class GetCourseLessonsQueryHandler 
    : IRequestHandler<GetCourseLessonsQuery, Result<List<LessonResponse>>>
{
    private readonly ILessonRepository _lessonRepository;

    public GetCourseLessonsQueryHandler(ILessonRepository lessonRepository)
    {
        _lessonRepository = lessonRepository;
    }

    public async Task<Result<List<LessonResponse>>> Handle(
        GetCourseLessonsQuery request, 
        CancellationToken cancellationToken)
    {
        var lessons = await _lessonRepository.FindAsync(
            l => l.CourseId == request.CourseId, 
            cancellationToken);

        var lessonResponses = lessons
            .OrderBy(l => l.SequenceNumber)
            .Select(l => new LessonResponse(
                l.Id,
                l.Title,
                l.ContentUrl,
                l.ContentType.ToString(),
                l.SequenceNumber,
                l.CourseId))
            .ToList();

        return lessonResponses;
    }
}
