using LearnCraft.Domain.Enums;
using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Users.Commands.RegisterUser;

public record RegisterUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    UserRole Role) : IRequest<Result<Guid>>;
