using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Users.Queries.Login;

public record LoginUserQuery(string Email, string Password) : IRequest<Result<string>>;
