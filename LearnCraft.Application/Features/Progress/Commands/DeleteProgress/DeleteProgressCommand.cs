using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Progress.Commands.DeleteProgress;

public record DeleteProgressCommand(Guid Id) : IRequest<Result>;
