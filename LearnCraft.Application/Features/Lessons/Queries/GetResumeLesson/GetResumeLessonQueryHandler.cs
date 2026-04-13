using LearnCraft.Application.Features.Lessons.Queries.GetLessonById;
using LearnCraft.Application.Interfaces.Repositories;
using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Lessons.Queries.GetResumeLesson;

public sealed class GetResumeLessonQueryHandler 
    : IRequestHandler<GetResumeLessonQuery, Result<LessonResponse>>
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly ILessonRepository _lessonRepository;
    private readonly IProgressRepository _progressRepository;

    public GetResumeLessonQueryHandler(
        IEnrollmentRepository enrollmentRepository, 
        ILessonRepository lessonRepository,
        IProgressRepository progressRepository)
    {
        _enrollmentRepository = enrollmentRepository;
        _lessonRepository = lessonRepository;
        _progressRepository = progressRepository;
    }

    public async Task<Result<LessonResponse>> Handle(
        GetResumeLessonQuery request, 
        CancellationToken cancellationToken)
    {
        var enrollments = await _enrollmentRepository.FindAsync(
            e => e.UserId == request.UserId && e.CourseId == request.CourseId, 
            cancellationToken);
        
        var enrollment = enrollments.FirstOrDefault();

        if (enrollment is null)
        {
            return Result.Failure<LessonResponse>(Error.NotFound("Enrollment.NotFound", "User is not enrolled in this course."));
        }

        var lessons = await _lessonRepository.FindAsync(l => l.CourseId == request.CourseId, cancellationToken);
        var progressRecords = await _progressRepository.FindAsync(p => p.EnrollmentId == enrollment.Id, cancellationToken);

        // Sort lessons by sequence
        var sortedLessons = lessons.OrderBy(l => l.SequenceNumber).ToList();

        if (!sortedLessons.Any())
        {
             return Result.Failure<LessonResponse>(Error.NotFound("Lessons.Empty", "This course has no lessons."));
        }

        // 1. Try to find the last accessed lesson that is not completed
        var lastAccessedInProgress = progressRecords
            .Where(p => !p.IsCompleted)
            .OrderByDescending(p => p.LastAccessedUtc)
            .FirstOrDefault();

        if (lastAccessedInProgress != null)
        {
            var lesson = sortedLessons.FirstOrDefault(l => l.Id == lastAccessedInProgress.LessonId);
            if (lesson != null)
            {
                return MapToResponse(lesson, sortedLessons);
            }
        }

        // 2. Try to find the first lesson that has no progress (not started)
        var startedLessonIds = progressRecords.Select(p => p.LessonId).ToHashSet();
        var firstNotStarted = sortedLessons.FirstOrDefault(l => !startedLessonIds.Contains(l.Id));

        if (firstNotStarted != null)
        {
            return MapToResponse(firstNotStarted, sortedLessons);
        }

        // 3. If all started, find the first not completed one (if any)
        var firstNotCompleted = sortedLessons
            .Join(progressRecords, l => l.Id, p => p.LessonId, (l, p) => new { Lesson = l, Progress = p })
            .Where(x => !x.Progress.IsCompleted)
            .OrderBy(x => x.Lesson.SequenceNumber)
            .Select(x => x.Lesson)
            .FirstOrDefault();

        if (firstNotCompleted != null)
        {
            return MapToResponse(firstNotCompleted, sortedLessons);
        }

        // 4. Default to the last lesson if all are completed
        return MapToResponse(sortedLessons.Last(), sortedLessons);
    }

    private LessonResponse MapToResponse(LearnCraft.Domain.Entities.Lesson lesson, List<LearnCraft.Domain.Entities.Lesson> allLessons)
    {
        var index = allLessons.IndexOf(lesson);
        return new LessonResponse(
            lesson.Id,
            lesson.Title,
            lesson.ContentUrl,
            lesson.ContentType.ToString(),
            lesson.SequenceNumber,
            lesson.CourseId,
            index < allLessons.Count - 1 ? allLessons[index + 1].Id : null,
            index > 0 ? allLessons[index - 1].Id : null);
    }
}
