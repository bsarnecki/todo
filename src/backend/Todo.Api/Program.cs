using AutoMapper;
using Azure.Identity;
using MediatR;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Options;
using Todo.Api.Commands;
using Todo.Api.Configuration;
using Todo.Api.Mapping;
using Todo.Api.Requests;
using Todo.Api.Responses;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddAutoMapper(x => x.AddProfile<DefaultProfile>());
builder.Services.Configure<TodoConfig>(
    builder.Configuration.GetSection(nameof(TodoConfig)));

builder.Services.AddAzureClients(clientBuilder =>
{
    clientBuilder.AddClient<CosmosClient, CosmosClientOptions>((c, t, s) =>
    {
        c.ApplicationName = "TodoApi";
        c.ConnectionMode = ConnectionMode.Direct;
        c.SerializerOptions = new CosmosSerializationOptions()
        {
            PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase,
            IgnoreNullValues = true
        };

        var options = s.GetRequiredService<IOptions<TodoConfig>>();

        return new CosmosClient(options.Value.CosmosEndpoint, t, c);
    });

    clientBuilder.UseCredential(new DefaultAzureCredential());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("reminders",
    async static (AddReminderRequest request, IMediator mediator, IMapper mapper, CancellationToken cancellationToken) =>
        await mediator.Send(mapper.Map<AddReminderCommand>(request), cancellationToken))
    .WithDescription("Adds new reminder for user")
    .ProducesProblem(400)
    .Produces(StatusCodes.Status204NoContent)
.WithOpenApi();

app.MapGet("reminders",
    async static (IMediator mediator, IMapper mapper, CancellationToken cancellationToken) =>
        mapper.Map<GetRemindersResponse>(await mediator.Send(new GetRemindersCommand(), cancellationToken)))
    .WithDescription("Returns reminders for user")
    .Produces(StatusCodes.Status200OK)
    .WithOpenApi();

app.Run();
