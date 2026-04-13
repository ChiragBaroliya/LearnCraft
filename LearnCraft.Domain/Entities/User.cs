using LearnCraft.Domain.Primitives;
using LearnCraft.Domain.Enums;

namespace LearnCraft.Domain.Entities;

public sealed class User : Entity
{
    private readonly List<Enrollment> _enrollments = new();
    private readonly List<Course> _taughtCourses = new();

    private User(Guid id, string firstName, string lastName, string email, string passwordHash, UserRole role)
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
    }

    private User() { }

    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public UserRole Role { get; private set; }

    public IReadOnlyCollection<Enrollment> Enrollments => _enrollments.AsReadOnly();
    public IReadOnlyCollection<Course> TaughtCourses => _taughtCourses.AsReadOnly();

    public static User Create(string firstName, string lastName, string email, string passwordHash, UserRole role)
    {
        return new User(Guid.NewGuid(), firstName, lastName, email, passwordHash, role);
    }

    public void UpdateProfile(string firstName, string lastName, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public void UpdatePassword(string passwordHash)
    {
        PasswordHash = passwordHash;
    }

    public void UpdateRole(UserRole role)
    {
        Role = role;
    }
}

