using LearnCraft.Domain.Entities;

namespace LearnCraft.Application.Interfaces.Authentication;

public interface IJwtProvider
{
    string Generate(User user);
}
