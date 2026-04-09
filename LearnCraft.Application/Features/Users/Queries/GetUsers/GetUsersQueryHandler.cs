using LearnCraft.Application.Common.Models;
using LearnCraft.Application.Features.Users.Queries.GetUserById;
using LearnCraft.Application.Interfaces.Repositories;
using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Users.Queries.GetUsers;

public sealed class GetUsersQueryHandler 
    : IRequestHandler<GetUsersQuery, Result<PagedResult<UserResponse>>>
{
    private readonly IUserRepository _userRepository;

    public GetUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<PagedResult<UserResponse>>> Handle(
        GetUsersQuery request, 
        CancellationToken cancellationToken)
    {
        var (items, totalCount) = await _userRepository.GetPagedAsync(
            request.PageNumber, 
            request.PageSize, 
            cancellationToken);

        var userResponses = items
            .Select(u => new UserResponse(
                u.Id,
                u.FirstName,
                u.LastName,
                u.Email,
                u.Role.ToString()))
            .ToList();

        return PagedResult<UserResponse>.Create(
            userResponses, 
            totalCount, 
            request.PageNumber, 
            request.PageSize);
    }
}
