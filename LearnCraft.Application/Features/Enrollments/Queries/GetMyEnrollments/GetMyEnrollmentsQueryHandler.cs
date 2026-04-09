using LearnCraft.Application.Interfaces.Repositories;
using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Enrollments.Queries.GetMyEnrollments;

public sealed class GetMyEnrollmentsQueryHandler 
    : IRequestHandler<GetMyEnrollmentsQuery, Result<List<MyEnrollmentResponse>>>
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly ICourseRepository _courseRepository;

    public GetMyEnrollmentsQueryHandler(
        IEnrollmentRepository enrollmentRepository, 
        ICourseRepository courseRepository)
    {
        _enrollmentRepository = enrollmentRepository;
        _courseRepository = courseRepository;
    }

    public async Task<Result<List<MyEnrollmentResponse>>> Handle(
        GetMyEnrollmentsQuery request, 
        CancellationToken cancellationToken)
    {
        var enrollments = await _enrollmentRepository.FindAsync(
            e => e.UserId == request.UserId, 
            cancellationToken);

        var courseIds = enrollments.Select(e => e.CourseId).Distinct().ToList();
        
        // This is a naive way for a small project. For large projects, a specific join query is better.
        var courses = await _courseRepository.FindAsync(
            c => courseIds.Contains(c.Id), 
            cancellationToken);

        var response = enrollments
            .Select(e => {
                var course = courses.FirstOrDefault(c => c.Id == e.CourseId);
                return new MyEnrollmentResponse(
                    e.Id,
                    e.CourseId,
                    course?.Title ?? "Unknown Course",
                    course?.ThumbnailUrl ?? string.Empty,
                    e.EnrolledAtUtc);
            })
            .ToList();

        return response;
    }
}
