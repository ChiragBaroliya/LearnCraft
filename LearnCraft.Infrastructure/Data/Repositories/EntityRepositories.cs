using LearnCraft.Application.Interfaces.Repositories;
using LearnCraft.Domain.Entities;

namespace LearnCraft.Infrastructure.Data.Repositories;

public sealed class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context) { }
}

public sealed class CourseRepository : GenericRepository<Course>, ICourseRepository
{
    public CourseRepository(ApplicationDbContext context) : base(context) { }
}

public sealed class LessonRepository : GenericRepository<Lesson>, ILessonRepository
{
    public LessonRepository(ApplicationDbContext context) : base(context) { }
}

public sealed class EnrollmentRepository : GenericRepository<Enrollment>, IEnrollmentRepository
{
    public EnrollmentRepository(ApplicationDbContext context) : base(context) { }
}

public sealed class ProgressRepository : GenericRepository<Progress>, IProgressRepository
{
    public ProgressRepository(ApplicationDbContext context) : base(context) { }
}
