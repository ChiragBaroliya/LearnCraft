using FluentValidation;

namespace LearnCraft.Application.Features.Courses.Commands.UpdateCourse;

public sealed class UpdateCourseValidator : AbstractValidator<UpdateCourseCommand>
{
    public UpdateCourseValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(2000);
        RuleFor(x => x.Price).NotNull();
        RuleFor(x => x.Category).NotEmpty();
    }
}
