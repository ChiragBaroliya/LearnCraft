using LearnCraft.Application.Common.Models;
using LearnCraft.Application.Features.Progress.Queries.GetProgressById;
using LearnCraft.Application.Interfaces.Repositories;
using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Progress.Queries.GetProgress;

public sealed class GetProgressQueryHandler 
    : IRequestHandler<GetProgressQuery, Result<PagedResult<ProgressResponse>>>
{
    private readonly IProgressRepository _progressRepository;

    public GetProgressQueryHandler(IProgressRepository progressRepository)
    {
        _progressRepository = progressRepository;
    }

    public async Task<Result<PagedResult<ProgressResponse>>> Handle(
        GetProgressQuery request, 
        CancellationToken cancellationToken)
    {
        var (items, totalCount) = await _progressRepository.GetPagedAsync(
            request.PageNumber, 
            request.PageSize, 
            cancellationToken);

        var progressResponses = items
            .Select(p => new ProgressResponse(
                p.Id,
                p.EnrollmentId,
                p.LessonId,
                p.IsCompleted,
                p.LastAccessedUtc))
            .ToList();

        return PagedResult<ProgressResponse>.Create(
            progressResponses, 
            totalCount, 
            request.PageNumber, 
            request.PageSize);
    }
}
