using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Courses.Queries.GetCourses;

public record GetCoursesQuery() : IRequest<Result<List<CourseResponse>>>;

public record CourseResponse(
    Guid Id, 
    string Title, 
    string Description, 
    decimal Price,
    string Category,
    string ThumbnailUrl,
    Guid InstructorId);
