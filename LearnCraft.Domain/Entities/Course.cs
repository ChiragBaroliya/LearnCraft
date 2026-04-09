using LearnCraft.Domain.Primitives;

namespace LearnCraft.Domain.Entities;

public sealed class Course : Entity
{
    private readonly List<Lesson> _lessons = new();
    private readonly List<Enrollment> _enrollments = new();

    private Course(Guid id, Guid instructorId, string title, string description, decimal price, string category, string thumbnailUrl)
        : base(id)
    {
        InstructorId = instructorId;
        Title = title;
        Description = description;
        Price = price;
        Category = category;
        ThumbnailUrl = thumbnailUrl;
        CreatedAtUtc = DateTime.UtcNow;
    }

    private Course() { }

    public Guid InstructorId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public string Category { get; private set; } = string.Empty;
    public string ThumbnailUrl { get; private set; } = string.Empty;
    public DateTime CreatedAtUtc { get; private set; }
    public bool IsDeleted { get; private set; }

    public IReadOnlyCollection<Lesson> Lessons => _lessons.AsReadOnly();
    public IReadOnlyCollection<Enrollment> Enrollments => _enrollments.AsReadOnly();

    public static Course Create(Guid instructorId, string title, string description, decimal price, string category, string thumbnailUrl)
    {
        return new Course(Guid.NewGuid(), instructorId, title, description, price, category, thumbnailUrl);
    }

    public void AddLesson(string title, string contentUrl, int sequenceNumber, Enums.ContentType contentType)
    {
        _lessons.Add(Lesson.Create(Id, title, contentUrl, sequenceNumber, contentType));
    }

    public void Delete() => IsDeleted = true;
}
