using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Enrollments.Commands.EnrollUser;

public record EnrollUserCommand(Guid UserId, Guid CourseId) : IRequest<Result<Guid>>;
