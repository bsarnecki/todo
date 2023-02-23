using MediatR;

namespace Todo.Api.Commands;
internal record GetRemindersCommand() : IRequest<IReadOnlyCollection<string>>;


