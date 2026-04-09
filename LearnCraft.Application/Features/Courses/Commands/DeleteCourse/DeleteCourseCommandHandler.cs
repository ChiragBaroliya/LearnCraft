using LearnCraft.Application.Interfaces.Data;
using LearnCraft.Application.Interfaces.Repositories;
using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Courses.Commands.DeleteCourse;

public sealed class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, Result>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
    {
        _courseRepository = courseRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(request.Id, cancellationToken);
        if (course is null)
        {
            return Result.Failure(new Error("Course.NotFound", "Course not found"));
        }

        course.Delete();
        _courseRepository.Update(course);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
