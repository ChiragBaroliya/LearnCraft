using LearnCraft.Application.Data;
using LearnCraft.Application.Interfaces.Data;
using LearnCraft.Application.Interfaces.Repositories;
using LearnCraft.Domain.Entities;
using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Courses.Commands.CreateCourse;

public sealed class CreateCourseCommandHandler 
    : IRequestHandler<CreateCourseCommand, Result<Guid>>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
    {
        _courseRepository = courseRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(
        CreateCourseCommand request, 
        CancellationToken cancellationToken)
    {
        var course = Course.Create(
            request.InstructorId,
            request.Title, 
            request.Description, 
            request.Price,
            request.Category,
            request.ThumbnailUrl);

        _courseRepository.Add(course);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return course.Id;
    }
}
