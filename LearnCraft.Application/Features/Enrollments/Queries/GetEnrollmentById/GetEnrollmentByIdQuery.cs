using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Enrollments.Queries.GetEnrollmentById;

public record GetEnrollmentByIdQuery(Guid Id) : IRequest<Result<EnrollmentResponse>>;

public record EnrollmentResponse(
    Guid Id,
    Guid UserId,
    Guid CourseId,
    DateTime EnrolledAtUtc);
