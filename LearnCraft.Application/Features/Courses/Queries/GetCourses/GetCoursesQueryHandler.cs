using LearnCraft.Application.Data;
using LearnCraft.Domain.Primitives;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnCraft.Application.Features.Courses.Queries.GetCourses;

public sealed class GetCoursesQueryHandler 
    : IRequestHandler<GetCoursesQuery, Result<List<CourseResponse>>>
{
    private readonly IApplicationDbContext _context;

    public GetCoursesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<CourseResponse>>> Handle(
        GetCoursesQuery request, 
        CancellationToken cancellationToken)
    {
        var courses = await _context.Courses
            .AsNoTracking()
            .Where(c => !c.IsDeleted)
            .Select(c => new CourseResponse(
                c.Id,
                c.Title,
                c.Description,
                c.Price,
                c.Category,
                c.ThumbnailUrl,
                c.InstructorId))
            .ToListAsync(cancellationToken);

        return courses;
    }
}
