using LearnCraft.Application.Interfaces.Data;
using LearnCraft.Application.Interfaces.Repositories;
using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Lessons.Commands.UpdateLesson;

public sealed class UpdateLessonCommandHandler 
    : IRequestHandler<UpdateLessonCommand, Result<Guid>>
{
    private readonly ILessonRepository _lessonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateLessonCommandHandler(ILessonRepository lessonRepository, IUnitOfWork unitOfWork)
    {
        _lessonRepository = lessonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(
        UpdateLessonCommand request, 
        CancellationToken cancellationToken)
    {
        var lesson = await _lessonRepository.GetByIdAsync(request.Id, cancellationToken);

        if (lesson is null)
        {
            return Result.Failure<Guid>(Error.NotFound("Lesson.NotFound", $"The lesson with Id {request.Id} was not found."));
        }

        lesson.Update(
            request.Title, 
            request.ContentUrl,
            request.ContentType);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return lesson.Id;
    }
}
