using LearnCraft.Domain.Entities;

namespace LearnCraft.Application.Interfaces.Repositories;

public interface IUserRepository : IGenericRepository<User> { }
public interface ICourseRepository : IGenericRepository<Course> { }
public interface ILessonRepository : IGenericRepository<Lesson> { }
public interface IEnrollmentRepository : IGenericRepository<Enrollment> { }
public interface IProgressRepository : IGenericRepository<Progress> { }
