using LearnCraft.Application.Common.Models;
using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Courses.Queries.GetCourses;

public record GetCoursesQuery(int PageNumber = 1, int PageSize = 10) 
    : IRequest<Result<PagedResult<CourseResponse>>>;

public record CourseResponse(
    Guid Id, 
    string Title, 
    string Description, 
    decimal Price,
    string Category,
    string ThumbnailUrl,
    Guid InstructorId);
