using LearnCraft.Application.Features.Courses.Commands.CreateCourse;
using LearnCraft.Application.Features.Courses.Commands.DeleteCourse;
using LearnCraft.Application.Features.Courses.Commands.UpdateCourse;
using LearnCraft.Application.Features.Courses.Commands.UpdateCourseStatus;
using MediatR;
using HotChocolate.Authorization;

namespace LearnCraft.API.GraphQL.Courses;

[ExtendObjectType("Mutation")]
public sealed class CourseMutations
{
    [Authorize(Roles = new[] { "Instructor", "Admin" })]
    public async Task<Guid> CreateCourse(
        [Service] ISender sender,
        CreateCourseCommand command,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            throw new Exception(result.Error.Message);
        }

        return result.Value;
    }

    [Authorize(Roles = new[] { "Instructor", "Admin" })]
    public async Task<Guid> UpdateCourse(
        [Service] ISender sender,
        UpdateCourseCommand command,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            throw new Exception(result.Error.Message);
        }

        return result.Value;
    }

    [Authorize(Roles = new[] { "Instructor", "Admin" })]
    public async Task<string> UpdateCourseStatus(
        [Service] ISender sender,
        UpdateCourseStatusCommand command,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            throw new Exception(result.Error.Message);
        }

        return "Course status updated successfully.";
    }

    [Authorize(Roles = new[] { "Instructor", "Admin" })]
    public async Task<Guid> DeleteCourse(
        [Service] ISender sender,
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new DeleteCourseCommand(id), cancellationToken);

        if (result.IsFailure)
        {
            throw new Exception(result.Error.Message);
        }

        return id;
    }
}
