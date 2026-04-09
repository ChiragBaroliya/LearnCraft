using LearnCraft.Application.Interfaces.Repositories;
using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Progress.Queries.GetProgressById;

public sealed class GetProgressByIdQueryHandler 
    : IRequestHandler<GetProgressByIdQuery, Result<ProgressResponse>>
{
    private readonly IProgressRepository _progressRepository;

    public GetProgressByIdQueryHandler(IProgressRepository progressRepository)
    {
        _progressRepository = progressRepository;
    }

    public async Task<Result<ProgressResponse>> Handle(
        GetProgressByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var progress = await _progressRepository.GetByIdAsync(request.Id, cancellationToken);

        if (progress is null)
        {
            return Result.Failure<ProgressResponse>(new Error("Progress.NotFound", "Progress not found"));
        }

        return new ProgressResponse(
            progress.Id,
            progress.EnrollmentId,
            progress.LessonId,
            progress.IsCompleted,
            progress.LastAccessedUtc);
    }
}
