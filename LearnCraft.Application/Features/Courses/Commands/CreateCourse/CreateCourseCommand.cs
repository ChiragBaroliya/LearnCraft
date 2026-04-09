using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Courses.Commands.CreateCourse;

public record CreateCourseCommand(
    Guid InstructorId,
    string Title, 
    string Description, 
    decimal Price,
    string Category,
    string ThumbnailUrl) 
    : IRequest<Result<Guid>>;
