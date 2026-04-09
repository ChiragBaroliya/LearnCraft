using LearnCraft.Application.Common.Models;
using LearnCraft.Application.Features.Enrollments.Queries.GetEnrollmentById;
using LearnCraft.Application.Interfaces.Repositories;
using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Enrollments.Queries.GetEnrollments;

public sealed class GetEnrollmentsQueryHandler 
    : IRequestHandler<GetEnrollmentsQuery, Result<PagedResult<EnrollmentResponse>>>
{
    private readonly IEnrollmentRepository _enrollmentRepository;

    public GetEnrollmentsQueryHandler(IEnrollmentRepository enrollmentRepository)
    {
        _enrollmentRepository = enrollmentRepository;
    }

    public async Task<Result<PagedResult<EnrollmentResponse>>> Handle(
        GetEnrollmentsQuery request, 
        CancellationToken cancellationToken)
    {
        var (items, totalCount) = await _enrollmentRepository.GetPagedAsync(
            request.PageNumber, 
            request.PageSize, 
            cancellationToken);

        var enrollmentResponses = items
            .Select(e => new EnrollmentResponse(
                e.Id,
                e.UserId,
                e.CourseId,
                e.EnrolledAtUtc))
            .ToList();

        return PagedResult<EnrollmentResponse>.Create(
            enrollmentResponses, 
            totalCount, 
            request.PageNumber, 
            request.PageSize);
    }
}
