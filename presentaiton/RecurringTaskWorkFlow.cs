using Elsa.Activities.Console;
using Elsa.Builders;

namespace Presentation;
public class RecurringTaskWorkflow : IWorkflow
{
    public static IServiceCollection ConfigureServices(IServiceCollection services)
    {
        services.AddElsa(elsa => elsa
            .AddConsoleActivities()
            .AddWorkflow<RecurringTaskWorkflow>());

        return services;
    }
    public void Build(IWorkflowBuilder builder) =>
        builder
            .StartWith<WriteLine>(activity => activity.Set(x => x.Text, "Hello, World!"))
            .Then<WriteLine>(activity => activity.Set(x => x.Text, "Elsa Workflow is running."))
            .Then(activity => Console.WriteLine("Hello, World!"))
            .Then<WriteLine>(activity => activity.Set(x => x.Text, "The workflow is completed."));
}
