using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Progress.Commands.CompleteLesson;

public record CompleteLessonCommand(Guid EnrollmentId, Guid LessonId) : IRequest<Result>;
