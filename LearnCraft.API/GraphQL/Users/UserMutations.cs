using HotChocolate.Authorization;
using LearnCraft.Application.Features.Users.Commands.DeleteUser;
using LearnCraft.Application.Features.Users.Commands.RegisterUser;
using LearnCraft.Application.Features.Users.Queries.Login;
using MediatR;

namespace LearnCraft.API.GraphQL.Users;

[ExtendObjectType("Mutation")]
public sealed class UserMutations
{
    public async Task<string> Login([Service] ISender sender, LoginUserQuery query, CancellationToken cancellationToken)
    {
        var result = await sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            throw new Exception(result.Error.Message);
        }

        return result.Value;
    }

    public async Task<Guid> Register([Service] ISender sender, RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            throw new Exception(result.Error.Message);
        }

        return result.Value;
    }

    [Authorize(Roles = new[] { "Admin" })]
    public async Task<Guid> DeleteUser([Service] ISender sender, Guid id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new DeleteUserCommand(id), cancellationToken);

        if (result.IsFailure)
        {
            throw new Exception(result.Error.Message);
        }

        return id;
    }
}
