using LearnCraft.Application.Features.Lessons.Queries.GetLessonById;
using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Courses.Queries.GetCourseLessons;

public record GetCourseLessonsQuery(Guid CourseId) : IRequest<Result<List<LessonResponse>>>;
