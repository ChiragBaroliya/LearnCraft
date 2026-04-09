using LearnCraft.Application.Interfaces.Data;
using LearnCraft.Application.Interfaces.Repositories;
using LearnCraft.Domain.Entities;
using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Lessons.Commands.AddLesson;

public sealed class AddLessonCommandHandler : IRequestHandler<AddLessonCommand, Result<Guid>>
{
    private readonly ICourseRepository _courseRepository;
    private readonly ILessonRepository _lessonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddLessonCommandHandler(
        ICourseRepository courseRepository, 
        ILessonRepository lessonRepository, 
        IUnitOfWork unitOfWork)
    {
        _courseRepository = courseRepository;
        _lessonRepository = lessonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(AddLessonCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(request.CourseId, cancellationToken);
        if (course is null)
        {
            return Result.Failure<Guid>(new Error("Course.NotFound", "Associated course not found."));
        }

        var lesson = Lesson.Create(
            request.CourseId,
            request.Title,
            request.ContentUrl,
            request.Sequence,
            request.ContentType);

        _lessonRepository.Add(lesson);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return lesson.Id;
    }
}
