using Elsa;
using Elsa.Activities.Console;
using Elsa.Builders;
using Elsa.Activities.ControlFlow;
using Elsa.Services.Workflows;
using Elsa.Activities;
using Elsa.Expressions;
using Elsa.Events;


namespace Presentation.WorkFlows;

public class LeaveRequestApprovalWorkflow : IWorkflow
{
    public void Build(IWorkflowBuilder builder)
    {
        builder
            .StartWith<WriteLine>(activity => activity.Set(x => x.Text, "Leave Request Submitted."))
            .Then<WriteLine>(activity => activity.Set(x => x.Text, "Sending request to Manager for approval."))
            .Then<IfElse>(ifElse => ifElse
                .Condition(context => GetManagerApproval())
                .When(OutcomeNames.True)
                    .Then<WriteLine>(activity => activity.Set(x => x.Text, "Manager approved the leave request."))
                    .Then<IfElse>(innerIfElse => innerIfElse
                        .Condition(context => GetHRApproval())
                        .When(OutcomeNames.True)
                            .Then<WriteLine>(activity => activity.Set(x => x.Text, "HR approved the leave request."))
                            .Then<WriteLine>(activity => activity.Set(x => x.Text, "Leave request is granted."))
                            .Then(activity => NotifyEmployee("Leave request approved"))
                            .Then(activity => NotifyHR("Leave request approved"))
                        .When(OutcomeNames.False)
                            .Then<WriteLine>(activity => activity.Set(x => x.Text, "HR rejected the leave request."))
                            .Then(activity => NotifyEmployee("Leave request rejected"))
                    )
                .When(OutcomeNames.False)
                    .Then<WriteLine>(activity => activity.Set(x => x.Text, "Manager rejected the leave request."))
                    .Then(activity => NotifyEmployee("Leave request rejected"))
            );
    }
}
