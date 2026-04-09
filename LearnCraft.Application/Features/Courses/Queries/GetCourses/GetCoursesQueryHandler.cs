using LearnCraft.Application.Common.Models;
using LearnCraft.Application.Interfaces.Repositories;
using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Courses.Queries.GetCourses;

public sealed class GetCoursesQueryHandler 
    : IRequestHandler<GetCoursesQuery, Result<PagedResult<CourseResponse>>>
{
    private readonly ICourseRepository _courseRepository;

    public GetCoursesQueryHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<Result<PagedResult<CourseResponse>>> Handle(
        GetCoursesQuery request, 
        CancellationToken cancellationToken)
    {
        var (items, totalCount) = await _courseRepository.GetPagedAsync(
            request.PageNumber, 
            request.PageSize, 
            cancellationToken);

        var courseResponses = items
            .Select(c => new CourseResponse(
                c.Id,
                c.Title,
                c.Description,
                c.Price,
                c.Category,
                c.ThumbnailUrl,
                c.InstructorId))
            .ToList();

        return PagedResult<CourseResponse>.Create(
            courseResponses, 
            totalCount, 
            request.PageNumber, 
            request.PageSize);
    }
}
