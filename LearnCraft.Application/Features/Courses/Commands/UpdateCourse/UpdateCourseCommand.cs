using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Courses.Commands.UpdateCourse;

public record UpdateCourseCommand(
    Guid Id,
    string Title, 
    string Description, 
    decimal Price,
    string Category,
    string ThumbnailUrl) 
    : IRequest<Result<Guid>>;
