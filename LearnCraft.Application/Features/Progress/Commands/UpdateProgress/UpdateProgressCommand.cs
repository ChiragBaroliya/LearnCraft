using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Progress.Commands.UpdateProgress;

public record UpdateProgressCommand(Guid EnrollmentId, Guid LessonId) : IRequest<Result>;
