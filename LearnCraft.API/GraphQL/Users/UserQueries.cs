using HotChocolate.Authorization;
using LearnCraft.Application.Common.Models;
using LearnCraft.Application.Features.Users.Queries.GetUserById;
using LearnCraft.Application.Features.Users.Queries.GetUsers;
using MediatR;

namespace LearnCraft.API.GraphQL.Users;

[ExtendObjectType("Query")]
public sealed class UserQueries
{
    [Authorize(Roles = new[] { "Admin" })]
    public async Task<PagedResult<UserResponse>> GetUsers(
         [Service] ISender sender,
            int pageNumber = 1,
            int pageSize = 10,
           CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetUsersQuery(pageNumber, pageSize), cancellationToken);

        if (result.IsFailure)
        {
            throw new Exception(result.Error.Message);
        }

        return result.Value;
    }

    [Authorize]
    public async Task<UserResponse> GetUserById([Service] ISender sender, Guid id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetUserByIdQuery(id), cancellationToken);

        if (result.IsFailure)
        {
            throw new Exception(result.Error.Message);
        }

        return result.Value;
    }
}
