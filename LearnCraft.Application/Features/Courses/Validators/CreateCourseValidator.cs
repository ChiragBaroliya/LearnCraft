using FluentValidation;
using LearnCraft.Application.Features.Courses.Commands.CreateCourse;

namespace LearnCraft.Application.Features.Courses.Validators;

public sealed class CreateCourseValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(2000);
        RuleFor(x => x.Price).NotNull().GreaterThanOrEqualTo(0);
        RuleFor(x => x.Category).NotEmpty();
        RuleFor(x => x.InstructorId).NotEmpty();
    }
}
