using LearnCraft.Application.Interfaces.Data;
using LearnCraft.Application.Interfaces.Repositories;
using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Lessons.Commands.ReorderLessons;

public sealed class ReorderLessonsCommandHandler 
    : IRequestHandler<ReorderLessonsCommand, Result>
{
    private readonly ILessonRepository _lessonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ReorderLessonsCommandHandler(ILessonRepository lessonRepository, IUnitOfWork unitOfWork)
    {
        _lessonRepository = lessonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        ReorderLessonsCommand request, 
        CancellationToken cancellationToken)
    {
        var lessons = await _lessonRepository.FindAsync(l => l.CourseId == request.CourseId, cancellationToken);

        if (lessons.Count != request.LessonIds.Count)
        {
            return Result.Failure(Error.Validation("Lessons.Mismatch", "The number of lessons provided does not match the course lessons."));
        }

        for (int i = 0; i < request.LessonIds.Count; i++)
        {
            var lessonId = request.LessonIds[i];
            var lesson = lessons.FirstOrDefault(l => l.Id == lessonId);
            
            if (lesson is null)
            {
                return Result.Failure(Error.NotFound("Lesson.NotFound", $"Lesson with Id {lessonId} was not found in this course."));
            }

            lesson.UpdateSequence(i + 1);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
