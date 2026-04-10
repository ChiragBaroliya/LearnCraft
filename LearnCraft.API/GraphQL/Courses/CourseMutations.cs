using LearnCraft.Application.Features.Courses.Commands.CreateCourse;
using LearnCraft.Application.Features.Courses.Commands.DeleteCourse;
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
