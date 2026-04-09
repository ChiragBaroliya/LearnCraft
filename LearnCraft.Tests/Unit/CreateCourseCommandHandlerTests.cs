using FluentAssertions;
using LearnCraft.Application.Interfaces.Data;
using LearnCraft.Application.Interfaces.Repositories;
using LearnCraft.Application.Features.Courses.Commands.CreateCourse;
using LearnCraft.Domain.Entities;
using Moq;
using Microsoft.EntityFrameworkCore;

namespace LearnCraft.Tests.Unit.Features.Courses.Commands;

public class CreateCourseCommandHandlerTests
{
    private readonly Mock<ICourseRepository> _courseRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly CreateCourseCommandHandler _handler;

    public CreateCourseCommandHandlerTests()
    {
        _courseRepositoryMock = new Mock<ICourseRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new CreateCourseCommandHandler(_courseRepositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_Should_AddCourseToRepository_And_Save()
    {
        // Arrange
        var instructorId = Guid.NewGuid();
        var command = new CreateCourseCommand(
            instructorId, 
            "Test Title", 
            "Test Description", 
            99.99m, 
            "Category", 
            "thumb.jpg");

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        _courseRepositoryMock.Verify(x => x.Add(It.IsAny<Course>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
