using LearnCraft.Application.Interfaces.Repositories;
using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Courses.Queries.GetCourseById;

public sealed class GetCourseByIdQueryHandler 
    : IRequestHandler<GetCourseByIdQuery, Result<CourseDetailsResponse>>
{
    private readonly ICourseRepository _courseRepository;

    public GetCourseByIdQueryHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<Result<CourseDetailsResponse>> Handle(
        GetCourseByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(request.Id, cancellationToken);

        if (course is null || course.IsDeleted)
        {
            return Result.Failure<CourseDetailsResponse>(new Error("Course.NotFound", "Course not found"));
        }

        return new CourseDetailsResponse(
            course.Id,
            course.Title,
            course.Description,
            course.Price,
            course.Category,
            course.ThumbnailUrl,
            course.InstructorId);
    }
}
