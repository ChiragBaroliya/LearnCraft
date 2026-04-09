using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Progress.Queries.GetProgressById;

public record GetProgressByIdQuery(Guid Id) : IRequest<Result<ProgressResponse>>;

public record ProgressResponse(
    Guid Id,
    Guid EnrollmentId,
    Guid LessonId,
    bool IsCompleted,
    DateTime LastAccessedUtc);
