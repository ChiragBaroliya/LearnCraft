using LearnCraft.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearnCraft.Application.Data;

public interface IApplicationDbContext
{
    DbSet<Course> Courses { get; }
    DbSet<User> Users { get; }
    DbSet<Lesson> Lessons { get; }
    DbSet<Enrollment> Enrollments { get; }
    DbSet<Progress> Progress { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
