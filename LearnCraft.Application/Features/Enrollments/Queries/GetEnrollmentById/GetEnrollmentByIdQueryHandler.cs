using LearnCraft.Application.Interfaces.Repositories;
using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Enrollments.Queries.GetEnrollmentById;

public sealed class GetEnrollmentByIdQueryHandler 
    : IRequestHandler<GetEnrollmentByIdQuery, Result<EnrollmentResponse>>
{
    private readonly IEnrollmentRepository _enrollmentRepository;

    public GetEnrollmentByIdQueryHandler(IEnrollmentRepository enrollmentRepository)
    {
        _enrollmentRepository = enrollmentRepository;
    }

    public async Task<Result<EnrollmentResponse>> Handle(
        GetEnrollmentByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var enrollment = await _enrollmentRepository.GetByIdAsync(request.Id, cancellationToken);

        if (enrollment is null)
        {
            return Result.Failure<EnrollmentResponse>(new Error("Enrollment.NotFound", "Enrollment not found"));
        }

        return new EnrollmentResponse(
            enrollment.Id,
            enrollment.UserId,
            enrollment.CourseId,
            enrollment.EnrolledAtUtc);
    }
}
