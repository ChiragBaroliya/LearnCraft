using LearnCraft.Application.Interfaces.Data;
using LearnCraft.Application.Interfaces.Repositories;
using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Courses.Commands.UpdateCourse;

public sealed class UpdateCourseCommandHandler 
    : IRequestHandler<UpdateCourseCommand, Result<Guid>>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
    {
        _courseRepository = courseRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(
        UpdateCourseCommand request, 
        CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(request.Id, cancellationToken);

        if (course is null)
        {
            return Result.Failure<Guid>(Error.NotFound("Course.NotFound", $"The course with Id {request.Id} was not found."));
        }

        course.Update(
            request.Title, 
            request.Description, 
            request.Price,
            request.Category,
            request.ThumbnailUrl);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return course.Id;
    }
}
