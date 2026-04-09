using FluentValidation;
using LearnCraft.Application.Features.Users.Queries.Login;

namespace LearnCraft.Application.Features.Users.Validators;

public sealed class LoginUserValidator : AbstractValidator<LoginUserQuery>
{
    public LoginUserValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}
