using Presentation;
using Elsa.Services;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Use the static method from RecurringTaskWorkflow to add Elsa workflows
        RecurringTaskWorkflow.ConfigureServices(builder.Services);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        // Map endpoints using services from DI container
        app.MapPost("/start-workflow", async (IBuildsAndStartsWorkflow workflowRunner) =>
        {
            var result = await workflowRunner.BuildAndStartWorkflowAsync<RecurringTaskWorkflow>();
            return Results.Ok(result);
        });

        app.Run();
    }
}
