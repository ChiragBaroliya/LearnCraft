using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Lessons.Commands.ReorderLessons;

public record ReorderLessonsCommand(
    Guid CourseId,
    List<Guid> LessonIds) 
    : IRequest<Result>;
