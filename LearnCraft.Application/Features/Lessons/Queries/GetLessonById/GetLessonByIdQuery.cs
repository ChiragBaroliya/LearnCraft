using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Lessons.Queries.GetLessonById;

public record GetLessonByIdQuery(Guid Id) : IRequest<Result<LessonResponse>>;

public record LessonResponse(
    Guid Id,
    string Title,
    string ContentUrl,
    string ContentType,
    int SequenceNumber,
    Guid CourseId,
    Guid? NextLessonId = null,
    Guid? PreviousLessonId = null);
