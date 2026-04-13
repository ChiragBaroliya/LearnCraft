using LearnCraft.Application.Interfaces.Data;
using LearnCraft.Application.Interfaces.Repositories;
using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Courses.Commands.UpdateCourseStatus;

public sealed class UpdateCourseStatusCommandHandler 
    : IRequestHandler<UpdateCourseStatusCommand, Result>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCourseStatusCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
    {
        _courseRepository = courseRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        UpdateCourseStatusCommand request, 
        CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(request.Id, cancellationToken);

        if (course is null)
        {
            return Result.Failure(Error.NotFound("Course.NotFound", $"The course with Id {request.Id} was not found."));
        }

        course.UpdateStatus(request.Status);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
