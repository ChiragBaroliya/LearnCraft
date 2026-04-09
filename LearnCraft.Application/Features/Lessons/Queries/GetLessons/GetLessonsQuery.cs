using LearnCraft.Application.Common.Models;
using LearnCraft.Application.Features.Lessons.Queries.GetLessonById;
using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Lessons.Queries.GetLessons;

public record GetLessonsQuery(int PageNumber = 1, int PageSize = 10) 
    : IRequest<Result<PagedResult<LessonResponse>>>;
