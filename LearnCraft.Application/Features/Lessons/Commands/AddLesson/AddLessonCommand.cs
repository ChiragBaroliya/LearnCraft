using LearnCraft.Domain.Enums;
using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Lessons.Commands.AddLesson;

public record AddLessonCommand(
    Guid CourseId,
    string Title,
    string ContentUrl,
    int Sequence,
    ContentType ContentType) : IRequest<Result<Guid>>;
