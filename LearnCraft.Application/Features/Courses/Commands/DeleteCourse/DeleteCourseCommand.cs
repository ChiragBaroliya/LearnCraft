using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Courses.Commands.DeleteCourse;

public record DeleteCourseCommand(Guid Id) : IRequest<Result>;
