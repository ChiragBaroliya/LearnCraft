using LearnCraft.Domain.Primitives;
using LearnCraft.Domain.Enums;

namespace LearnCraft.Domain.Entities;

public sealed class Lesson : Entity
{
    private Lesson(Guid id, Guid courseId, string title, string contentUrl, int sequenceNumber, ContentType contentType)
        : base(id)
    {
        CourseId = courseId;
        Title = title;
        ContentUrl = contentUrl;
        SequenceNumber = sequenceNumber;
        ContentType = contentType;
    }

    private Lesson() { }

    public Guid CourseId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string ContentUrl { get; private set; } = string.Empty;
    public int SequenceNumber { get; private set; }
    public ContentType ContentType { get; private set; }

    public static Lesson Create(Guid courseId, string title, string contentUrl, int sequenceNumber, ContentType contentType)
    {
        return new Lesson(Guid.NewGuid(), courseId, title, contentUrl, sequenceNumber, contentType);
    }

    public void Update(string title, string contentUrl, ContentType contentType)
    {
        Title = title;
        ContentUrl = contentUrl;
        ContentType = contentType;
    }

    public void UpdateSequence(int sequenceNumber)
    {
        SequenceNumber = sequenceNumber;
    }
}

