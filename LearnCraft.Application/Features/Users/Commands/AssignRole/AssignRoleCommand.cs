using LearnCraft.Domain.Enums;
using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Users.Commands.AssignRole;

public record AssignRoleCommand(
    Guid UserId,
    UserRole Role) 
    : IRequest<Result>;
