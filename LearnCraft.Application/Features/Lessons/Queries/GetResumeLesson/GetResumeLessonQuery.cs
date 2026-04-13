using LearnCraft.Application.Features.Lessons.Queries.GetLessonById;
using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Lessons.Queries.GetResumeLesson;

public record GetResumeLessonQuery(Guid CourseId, Guid UserId) 
    : IRequest<Result<LessonResponse>>;
