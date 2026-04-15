using LearnCraft.Application.Interfaces.Authentication;
using LearnCraft.Application.Interfaces.Repositories;
using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Users.Queries.Login;

public sealed class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, Result<string>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IPasswordHasher _passwordHasher;

    public LoginUserQueryHandler(IUserRepository userRepository, IJwtProvider jwtProvider, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<string>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {

        var users = await _userRepository.FindAsync(x => x.Email == request.Email, cancellationToken);
        var user = users.FirstOrDefault();

        if (user is null)
        {
            return Result.Failure<string>(new Error("User.NotFound", "Invalid email or password."));
        }

        if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
        {
            return Result.Failure<string>(new Error("User.InvalidPassword", "Invalid email or password."));
        }

        var token = _jwtProvider.Generate(user);

        return token;
    }
}
