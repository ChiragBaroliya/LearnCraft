using LearnCraft.Domain.Primitives;

namespace LearnCraft.Domain.Entities;

public sealed class Progress : Entity
{
    private Progress(Guid id, Guid enrollmentId, Guid lessonId)
        : base(id)
    {
        EnrollmentId = enrollmentId;
        LessonId = lessonId;
        IsCompleted = false;
        LastAccessedUtc = DateTime.UtcNow;
    }

    private Progress() { }

    public Guid EnrollmentId { get; private set; }
    public Guid LessonId { get; private set; }
    public bool IsCompleted { get; private set; }
    public DateTime LastAccessedUtc { get; private set; }

    public static Progress Create(Guid enrollmentId, Guid lessonId)
    {
        return new Progress(Guid.NewGuid(), enrollmentId, lessonId);
    }

    public void MarkAsCompleted()
    {
        IsCompleted = true;
        LastAccessedUtc = DateTime.UtcNow;
    }

    public void UpdateLastAccessed()
    {
        LastAccessedUtc = DateTime.UtcNow;
    }
}
