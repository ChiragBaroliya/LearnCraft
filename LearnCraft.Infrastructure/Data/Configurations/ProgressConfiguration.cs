using LearnCraft.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnCraft.Infrastructure.Data.Configurations;

public sealed class ProgressConfiguration : IEntityTypeConfiguration<Progress>
{
    public void Configure(EntityTypeBuilder<Progress> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasIndex(p => new { p.EnrollmentId, p.LessonId }).IsUnique();

        builder.Property(p => p.IsCompleted).HasDefaultValue(false);

        builder.Property(p => p.LastAccessedUtc).IsRequired();
    }
}
