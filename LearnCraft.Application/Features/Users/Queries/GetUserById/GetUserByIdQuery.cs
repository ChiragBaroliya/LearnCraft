using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Users.Queries.GetUserById;

public record GetUserByIdQuery(Guid Id) : IRequest<Result<UserResponse>>;

public record UserResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Role);
