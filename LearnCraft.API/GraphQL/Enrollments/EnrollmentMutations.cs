using LearnCraft.Application.Features.Enrollments.Commands.DeleteEnrollment;
using LearnCraft.Application.Features.Enrollments.Commands.EnrollUser;
using MediatR;
using HotChocolate.Authorization;

namespace LearnCraft.API.GraphQL.Enrollments;

[ExtendObjectType("Mutation")]
public sealed class EnrollmentMutations
{
    [Authorize]
    public async Task<Guid> Enroll(
        [Service] ISender sender,
        EnrollUserCommand command,
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
    public async Task<Guid> DeleteEnrollment(
        [Service] ISender sender,
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new DeleteEnrollmentCommand(id), cancellationToken);

        if (result.IsFailure)
        {
            throw new Exception(result.Error.Message);
        }

        return id;
    }
}
