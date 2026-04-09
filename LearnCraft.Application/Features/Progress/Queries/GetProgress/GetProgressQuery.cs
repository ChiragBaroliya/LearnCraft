using LearnCraft.Application.Common.Models;
using LearnCraft.Application.Features.Progress.Queries.GetProgressById;
using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Progress.Queries.GetProgress;

public record GetProgressQuery(int PageNumber = 1, int PageSize = 10) 
    : IRequest<Result<PagedResult<ProgressResponse>>>;
