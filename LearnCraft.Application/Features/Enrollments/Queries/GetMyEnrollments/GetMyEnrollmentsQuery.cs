using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Enrollments.Queries.GetMyEnrollments;

public record GetMyEnrollmentsQuery(Guid UserId) : IRequest<Result<List<MyEnrollmentResponse>>>;

public record MyEnrollmentResponse(
    Guid EnrollmentId,
    Guid CourseId,
    string CourseTitle,
    string CourseThumbnailUrl,
    DateTime EnrolledAtUtc);
