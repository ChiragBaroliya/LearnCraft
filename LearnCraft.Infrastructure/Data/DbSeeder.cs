using LearnCraft.Domain.Entities;
using LearnCraft.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace LearnCraft.Infrastructure.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (await context.Users.AnyAsync())
        {
            return;
        }

        // Seed Users
        var admin = User.Create("Admin", "User", "admin@learncraft.com", "$2a$12$R9h/lIPz0bouvm6NjG.Zp.99W9zfMTuHH6uTbNtyD22Y1rBqK.432", UserRole.Admin);
        var instructor = User.Create("John", "Instructor", "instructor@learncraft.com", "$2a$12$R9h/lIPz0bouvm6NjG.Zp.99W9zfMTuHH6uTbNtyD22Y1rBqK.432", UserRole.Instructor);
        var student = User.Create("Jane", "Student", "student@learncraft.com", "$2a$12$R9h/lIPz0bouvm6NjG.Zp.99W9zfMTuHH6uTbNtyD22Y1rBqK.432", UserRole.Student);

        context.Users.AddRange(admin, instructor, student);

        // Seed Course
        var course = Course.Create(
            instructor.Id,
            "Mastering Clean Architecture with .NET 8",
            "A comprehensive guide to building scalable systems using MediatR, EF Core, and DDD.",
            199.99m,
            "Software Development",
            "https://images.unsplash.com/photo-1542831371-29b0f74f9713");

        course.AddLesson("Project Introduction", "https://learncraft.blob/intro.mp4", 1, ContentType.Video);
        course.AddLesson("Setting up the Domain Layer", "https://learncraft.blob/domain.pdf", 2, ContentType.Document);

        context.Courses.Add(course);

        // Seed Enrollment for student
        var enrollment = Enrollment.Create(student.Id, course.Id);
        context.Enrollments.Add(enrollment);

        await context.SaveChangesAsync();
    }
}
