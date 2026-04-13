using LearnCraft.Domain.Enums;
using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Lessons.Commands.UpdateLesson;

public record UpdateLessonCommand(
    Guid Id,
    string Title, 
    string ContentUrl,
    ContentType ContentType) 
    : IRequest<Result<Guid>>;
