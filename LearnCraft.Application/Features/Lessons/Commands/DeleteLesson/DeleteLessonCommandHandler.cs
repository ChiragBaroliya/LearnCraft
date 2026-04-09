using LearnCraft.Application.Interfaces.Data;
using LearnCraft.Application.Interfaces.Repositories;
using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Lessons.Commands.DeleteLesson;

public sealed class DeleteLessonCommandHandler 
    : IRequestHandler<DeleteLessonCommand, Result>
{
    private readonly ILessonRepository _lessonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteLessonCommandHandler(ILessonRepository lessonRepository, IUnitOfWork unitOfWork)
    {
        _lessonRepository = lessonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        DeleteLessonCommand request, 
        CancellationToken cancellationToken)
    {
        var lesson = await _lessonRepository.GetByIdAsync(request.Id, cancellationToken);

        if (lesson is null)
        {
            return Result.Failure(new Error("Lesson.NotFound", "Lesson not found"));
        }

        _lessonRepository.Delete(lesson);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
