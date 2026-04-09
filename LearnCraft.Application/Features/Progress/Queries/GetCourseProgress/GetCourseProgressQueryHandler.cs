using LearnCraft.Application.Interfaces.Repositories;
using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Progress.Queries.GetCourseProgress;

public sealed class GetCourseProgressQueryHandler 
    : IRequestHandler<GetCourseProgressQuery, Result<CourseProgressResponse>>
{
    private readonly IProgressRepository _progressRepository;
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly ILessonRepository _lessonRepository;

    public GetCourseProgressQueryHandler(
        IProgressRepository progressRepository, 
        IEnrollmentRepository enrollmentRepository,
        ILessonRepository lessonRepository)
    {
        _progressRepository = progressRepository;
        _enrollmentRepository = enrollmentRepository;
        _lessonRepository = lessonRepository;
    }

    public async Task<Result<CourseProgressResponse>> Handle(
        GetCourseProgressQuery request, 
        CancellationToken cancellationToken)
    {
        var enrollment = (await _enrollmentRepository.FindAsync(
            e => e.UserId == request.UserId && e.CourseId == request.CourseId, 
            cancellationToken)).FirstOrDefault();

        if (enrollment is null)
        {
            return Result.Failure<CourseProgressResponse>(new Error("Enrollment.NotFound", "User is not enrolled in this course."));
        }

        var lessons = await _lessonRepository.FindAsync(
            l => l.CourseId == request.CourseId, 
            cancellationToken);

        var progressRecords = await _progressRepository.FindAsync(
            p => p.EnrollmentId == enrollment.Id, 
            cancellationToken);

        var lessonProgress = lessons
            .OrderBy(l => l.SequenceNumber)
            .Select(l => {
                var progress = progressRecords.FirstOrDefault(p => p.LessonId == l.Id);
                return new LessonProgressResponse(
                    l.Id,
                    l.Title,
                    progress?.IsCompleted ?? false,
                    progress?.LastAccessedUtc);
            })
            .ToList();

        var completedCount = lessonProgress.Count(lp => lp.IsCompleted);
        var totalCount = lessonProgress.Count;
        var percentage = totalCount > 0 ? (decimal)completedCount / totalCount * 100 : 0;

        return new CourseProgressResponse(percentage, lessonProgress);
    }
}
