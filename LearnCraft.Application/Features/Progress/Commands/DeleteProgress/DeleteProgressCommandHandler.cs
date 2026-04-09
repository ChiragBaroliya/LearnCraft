using LearnCraft.Application.Interfaces.Data;
using LearnCraft.Application.Interfaces.Repositories;
using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Progress.Commands.DeleteProgress;

public sealed class DeleteProgressCommandHandler 
    : IRequestHandler<DeleteProgressCommand, Result>
{
    private readonly IProgressRepository _progressRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProgressCommandHandler(IProgressRepository progressRepository, IUnitOfWork unitOfWork)
    {
        _progressRepository = progressRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        DeleteProgressCommand request, 
        CancellationToken cancellationToken)
    {
        var progress = await _progressRepository.GetByIdAsync(request.Id, cancellationToken);

        if (progress is null)
        {
            return Result.Failure(new Error("Progress.NotFound", "Progress not found"));
        }

        _progressRepository.Delete(progress);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
