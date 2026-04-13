using LearnCraft.Domain.Enums;
using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Courses.Commands.UpdateCourseStatus;

public record UpdateCourseStatusCommand(
    Guid Id,
    CourseStatus Status) 
    : IRequest<Result>;
