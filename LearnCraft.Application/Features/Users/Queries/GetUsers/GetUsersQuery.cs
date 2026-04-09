using LearnCraft.Application.Common.Models;
using LearnCraft.Application.Features.Users.Queries.GetUserById;
using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Users.Queries.GetUsers;

public record GetUsersQuery(int PageNumber = 1, int PageSize = 10) 
    : IRequest<Result<PagedResult<UserResponse>>>;
