using LearnCraft.Domain.Primitives;

namespace LearnCraft.Domain.Entities;

public sealed class Enrollment : Entity
{
    private readonly List<Progress> _progress = new();

    private Enrollment(Guid id, Guid userId, Guid courseId)
        : base(id)
    {
        UserId = userId;
        CourseId = courseId;
        EnrolledAtUtc = DateTime.UtcNow;
    }

    private Enrollment() { }

    public Guid UserId { get; private set; }
    public Guid CourseId { get; private set; }
    public DateTime EnrolledAtUtc { get; private set; }

    public IReadOnlyCollection<Progress> Progress => _progress.AsReadOnly();

    public static Enrollment Create(Guid userId, Guid courseId)
    {
        return new Enrollment(Guid.NewGuid(), userId, courseId);
    }
}
