using LearnCraft.Application.Interfaces.Data;
using LearnCraft.Application.Interfaces.Repositories;
using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Enrollments.Commands.DeleteEnrollment;

public sealed class DeleteEnrollmentCommandHandler 
    : IRequestHandler<DeleteEnrollmentCommand, Result>
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteEnrollmentCommandHandler(IEnrollmentRepository enrollmentRepository, IUnitOfWork unitOfWork)
    {
        _enrollmentRepository = enrollmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        DeleteEnrollmentCommand request, 
        CancellationToken cancellationToken)
    {
        var enrollment = await _enrollmentRepository.GetByIdAsync(request.Id, cancellationToken);

        if (enrollment is null)
        {
            return Result.Failure(new Error("Enrollment.NotFound", "Enrollment not found"));
        }

        _enrollmentRepository.Delete(enrollment);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
