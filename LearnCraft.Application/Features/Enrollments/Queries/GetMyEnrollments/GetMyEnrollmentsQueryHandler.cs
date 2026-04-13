using LearnCraft.Application.Features.Courses.Queries.GetCourses;
using LearnCraft.Application.Interfaces.Repositories;
using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Enrollments.Queries.GetMyEnrollments;

public sealed class GetMyEnrollmentsQueryHandler 
    : IRequestHandler<GetMyEnrollmentsQuery, Result<List<CourseResponse>>>
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

    public async Task<Result<List<CourseResponse>>> Handle(
        GetMyEnrollmentsQuery request, 
        CancellationToken cancellationToken)
    {
        var enrollments = await _enrollmentRepository.FindAsync(e => e.UserId == request.UserId, cancellationToken);
        
        var courseIds = enrollments.Select(e => e.CourseId).ToList();
        
        var courses = await _courseRepository.FindAsync(c => courseIds.Contains(c.Id), cancellationToken);

        var response = courses.Select(c => new CourseResponse(
            c.Id,
            c.Title,
            c.Description,
            c.Price,
            c.Category,
            c.ThumbnailUrl,
            c.InstructorId)).ToList();

        return response;
    }
}
