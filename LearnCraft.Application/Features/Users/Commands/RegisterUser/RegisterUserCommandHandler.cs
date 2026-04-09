using LearnCraft.Application.Interfaces.Authentication;
using LearnCraft.Application.Interfaces.Data;
using LearnCraft.Application.Interfaces.Repositories;
using LearnCraft.Domain.Entities;
using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Users.Commands.RegisterUser;

public sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<Guid>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var existingUsers = await _userRepository.FindAsync(x => x.Email == request.Email, cancellationToken);
        if (existingUsers.Any())
        {
            return Result.Failure<Guid>(new Error("User.Conflict", "Email is already in use."));
        }

        var user = User.Create(
            request.FirstName,
            request.LastName,
            request.Email,
            _passwordHasher.Hash(request.Password),
            request.Role);

        _userRepository.Add(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
