using Testcontainers.PostgreSql;
using Microsoft.EntityFrameworkCore;
using LearnCraft.Infrastructure.Data;
using LearnCraft.Domain.Entities;
using FluentAssertions;

namespace LearnCraft.Tests.Integration;

public class CourseIntegrationTests : IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .Build();

    public async Task InitializeAsync() => await _dbContainer.StartAsync();

    public async Task DisposeAsync() => await _dbContainer.DisposeAsync();

    [Fact]
    public async Task CreateCourse_Should_PersistToDatabase()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(_dbContainer.GetConnectionString())
            .Options;

        using var context = new ApplicationDbContext(options);
        await context.Database.EnsureCreatedAsync();

        var instructorId = Guid.NewGuid();
        var course = Course.Create(
            instructorId, 
            "Integration Title", 
            "Integration Desc", 
            49.99m, 
            "Category", 
            "thumb.jpg");

        // Act
        context.Courses.Add(course);
        await context.SaveChangesAsync();

        // Assert
        var savedCourse = await context.Courses.FirstOrDefaultAsync(c => c.Id == course.Id);
        savedCourse.Should().NotBeNull();
        savedCourse!.Title.Should().Be("Integration Title");
    }
}
