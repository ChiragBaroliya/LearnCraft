using LearnCraft.Application.Features.Progress.Commands.CompleteLesson;
using LearnCraft.Application.Features.Progress.Commands.DeleteProgress;
using LearnCraft.Application.Features.Progress.Commands.UpdateProgress;
using MediatR;
using HotChocolate.Authorization;

namespace LearnCraft.API.GraphQL.Progress;

[ExtendObjectType("Mutation")]
public sealed class ProgressMutations
{
    [Authorize]
    public async Task<bool> TrackProgress(
        [Service] ISender sender,
        UpdateProgressCommand command, 
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            throw new Exception(result.Error.Message);
        }

        return true;
    }

    [Authorize]
    public async Task<bool> CompleteLesson(
         [Service] ISender sender,
        CompleteLessonCommand command, 
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            throw new Exception(result.Error.Message);
        }

        return true;
    }

    [Authorize(Roles = new[] { "Instructor", "Admin" })]
    public async Task<Guid> DeleteProgress(
        [Service] ISender sender, 
        Guid id, 
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new DeleteProgressCommand(id), cancellationToken);

        if (result.IsFailure)
        {
            throw new Exception(result.Error.Message);
        }

        return id;
    }
}
