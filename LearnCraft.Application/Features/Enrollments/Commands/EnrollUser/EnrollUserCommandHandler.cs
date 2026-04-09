using LearnCraft.Application.Interfaces.Data;
using LearnCraft.Application.Interfaces.Repositories;
using LearnCraft.Domain.Entities;
using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Enrollments.Commands.EnrollUser;

public sealed class EnrollUserCommandHandler : IRequestHandler<EnrollUserCommand, Result<Guid>>
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EnrollUserCommandHandler(
        IEnrollmentRepository enrollmentRepository, 
        ICourseRepository courseRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _enrollmentRepository = enrollmentRepository;
        _courseRepository = courseRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(EnrollUserCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(request.CourseId, cancellationToken);
        if (course is null) return Result.Failure<Guid>(new Error("Course.NotFound", "Course not found."));

        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null) return Result.Failure<Guid>(new Error("User.NotFound", "User not found."));

        var existing = await _enrollmentRepository.FindAsync(x => x.UserId == request.UserId && x.CourseId == request.CourseId, cancellationToken);
        if (existing.Any()) return Result.Failure<Guid>(new Error("Enrollment.Conflict", "User already enrolled in this course."));

        var enrollment = Enrollment.Create(request.UserId, request.CourseId);
        _enrollmentRepository.Add(enrollment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return enrollment.Id;
    }
}
