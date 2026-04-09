using LearnCraft.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnCraft.Infrastructure.Data.Configurations;

public sealed class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.HasKey(l => l.Id);

        builder.Property(l => l.Title).HasMaxLength(200).IsRequired();
        builder.Property(l => l.ContentUrl).HasMaxLength(500).IsRequired();
        builder.Property(l => l.SequenceNumber).IsRequired();
        builder.Property(l => l.ContentType).IsRequired();

        builder.HasIndex(l => new { l.CourseId, l.SequenceNumber }).IsUnique();
    }
}
