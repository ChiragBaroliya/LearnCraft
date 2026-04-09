using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Courses.Queries.GetCourseById;

public record GetCourseByIdQuery(Guid Id) : IRequest<Result<CourseDetailsResponse>>;

public record CourseDetailsResponse(
    Guid Id,
    string Title,
    string Description,
    decimal Price,
    string Category,
    string ThumbnailUrl,
    Guid InstructorId);
