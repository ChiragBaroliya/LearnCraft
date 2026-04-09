using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Lessons.Commands.DeleteLesson;

public record DeleteLessonCommand(Guid Id) : IRequest<Result>;
