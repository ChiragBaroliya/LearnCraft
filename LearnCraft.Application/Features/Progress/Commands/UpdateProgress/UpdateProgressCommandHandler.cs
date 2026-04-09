using LearnCraft.Application.Interfaces.Data;
using LearnCraft.Application.Interfaces.Repositories;
using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Progress.Commands.UpdateProgress;

public sealed class UpdateProgressCommandHandler : IRequestHandler<UpdateProgressCommand, Result>
{
    private readonly IProgressRepository _progressRepository;
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly ILessonRepository _lessonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProgressCommandHandler(
        IProgressRepository progressRepository, 
        IEnrollmentRepository enrollmentRepository,
        ILessonRepository lessonRepository,
        IUnitOfWork unitOfWork)
    {
        _progressRepository = progressRepository;
        _enrollmentRepository = enrollmentRepository;
        _lessonRepository = lessonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateProgressCommand request, CancellationToken cancellationToken)
    {
        var enrollment = await _enrollmentRepository.GetByIdAsync(request.EnrollmentId, cancellationToken);
        if (enrollment is null) return Result.Failure(new Error("Enrollment.NotFound", "Enrollment not found."));

        var lesson = await _lessonRepository.GetByIdAsync(request.LessonId, cancellationToken);
        if (lesson is null) return Result.Failure(new Error("Lesson.NotFound", "Lesson not found."));

        var progress = (await _progressRepository.FindAsync(
            p => p.EnrollmentId == request.EnrollmentId && p.LessonId == request.LessonId, 
            cancellationToken)).FirstOrDefault();

        if (progress is null)
        {
            progress = LearnCraft.Domain.Entities.Progress.Create(request.EnrollmentId, request.LessonId);
            _progressRepository.Add(progress);
        }
        else
        {
            progress.UpdateLastAccessed();
        }
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
