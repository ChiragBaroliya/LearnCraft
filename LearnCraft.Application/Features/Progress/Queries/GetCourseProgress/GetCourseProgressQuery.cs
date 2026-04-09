using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Progress.Queries.GetCourseProgress;

public record GetCourseProgressQuery(Guid CourseId, Guid UserId) : IRequest<Result<CourseProgressResponse>>;

public record CourseProgressResponse(
    decimal CompletionPercentage,
    List<LessonProgressResponse> Lessons);

public record LessonProgressResponse(
    Guid LessonId,
    string Title,
    bool IsCompleted,
    DateTime? LastAccessedUtc);
