using AutoMapper;
using Todo.Api.Responses;

namespace Todo.Api.Mapping.TypeConverters;

internal class ReminderResponseConverter : ITypeConverter<IReadOnlyCollection<string>, GetRemindersResponse>
{
    public GetRemindersResponse Convert(IReadOnlyCollection<string> source, 
        GetRemindersResponse destination, 
        ResolutionContext context)
    {
        if (source == null)
        {
            return new GetRemindersResponse(new List<string>(0));
        }
        
        return new GetRemindersResponse(source);
    }
}