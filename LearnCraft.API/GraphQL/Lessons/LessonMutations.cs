using LearnCraft.Application.Features.Lessons.Commands.AddLesson;
using LearnCraft.Application.Features.Lessons.Commands.DeleteLesson;
using LearnCraft.Application.Features.Lessons.Commands.UpdateLesson;
using LearnCraft.Application.Features.Lessons.Commands.ReorderLessons;
using MediatR;
using HotChocolate.Authorization;

namespace LearnCraft.API.GraphQL.Lessons;

[ExtendObjectType("Mutation")]
public sealed class LessonMutations
{
    [Authorize(Roles = new[] { "Instructor", "Admin" })]
    public async Task<Guid> AddLesson(
        [Service] ISender sender,
        AddLessonCommand command,
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
    public async Task<Guid> UpdateLesson(
        [Service] ISender sender,
        UpdateLessonCommand command,
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
    public async Task<string> ReorderLessons(
        [Service] ISender sender,
        ReorderLessonsCommand command,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            throw new Exception(result.Error.Message);
        }

        return "Lessons reordered successfully.";
    }

    [Authorize(Roles = new[] { "Instructor", "Admin" })]
    public async Task<Guid> DeleteLesson(
        [Service] ISender sender,
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new DeleteLessonCommand(id), cancellationToken);

        if (result.IsFailure)
        {
            throw new Exception(result.Error.Message);
        }

        return id;
    }
}
