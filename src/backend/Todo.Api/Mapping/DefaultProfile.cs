using AutoMapper;
using Todo.Api.Commands;
using Todo.Api.Mapping.TypeConverters;
using Todo.Api.Requests;
using Todo.Api.Responses;

namespace Todo.Api.Mapping;

internal class DefaultProfile : Profile
{
    public DefaultProfile()
    {
        CreateMap<AddReminderRequest, AddReminderCommand>();
        CreateMap<IReadOnlyCollection<string>, GetRemindersResponse>()
            .ConvertUsing<ReminderResponseConverter>();
    }
}

