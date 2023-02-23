using MediatR;
using Todo.Api.Commands;

namespace Todo.Api.Handlers;

internal class AddReminderHandler : IRequestHandler<AddReminderCommand>
{
    public Task Handle(AddReminderCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}