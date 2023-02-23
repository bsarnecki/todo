using MediatR;
using Todo.Api.Commands;

namespace Todo.Api.Handlers;
internal class GetRemindersHandler : IRequestHandler<GetRemindersCommand, IReadOnlyCollection<string>>
{
    public Task<IReadOnlyCollection<string>> Handle(GetRemindersCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}


