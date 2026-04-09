using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Enrollments.Commands.DeleteEnrollment;

public record DeleteEnrollmentCommand(Guid Id) : IRequest<Result>;
