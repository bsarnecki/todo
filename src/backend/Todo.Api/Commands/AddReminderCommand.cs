using MediatR;

namespace Todo.Api.Commands;

internal record AddReminderCommand(string Content) : IRequest;