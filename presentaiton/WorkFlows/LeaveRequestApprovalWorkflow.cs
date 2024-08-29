using Elsa;
using Elsa.Activities.Console;
using Elsa.Builders;
using Elsa.Activities.ControlFlow;
using Elsa.Services.Workflows;
using Elsa.Activities;
using Elsa.Expressions;
using Elsa.Events;
using Elsa.Services.Models;
using System;


namespace Presentation.WorkFlows;

public class LeaveRequestApprovalWorkflow : IWorkflow
{
    public void Build(IWorkflowBuilder builder)
    {
        builder
               .StartWith<WriteLine>(activity => activity.Set(x => x.Text, "Leave Request Submitted."))
               .Then<WriteLine>(activity => activity.Set(x => x.Text, "Sending request to Manager for approval."))
               .Then<If>(ifBuilder => ifBuilder
                   .WithCondition(context => GetManagerApproval())
                   .Then(then => then
                       .StartWith<WriteLine>(activity => activity.Set(x => x.Text, "Manager approved the leave request."))
                       .Then<If>(innerIfBuilder => innerIfBuilder
                           .WithCondition(context => GetHRApproval())
                           .Then(then => then
                               .StartWith<WriteLine>(activity => activity.Set(x => x.Text, "HR approved the leave request."))
                               .Then<WriteLine>(activity => activity.Set(x => x.Text, "Leave request is granted."))
                               .Then((ActivityExecutionContext activity) => activity
                                   .StartWith<WriteLine>(x => x.Set(y => y.Text, "Leave request approved"))
                               )
                               .Then(activity => activity
                                   .StartWith<WriteLine>(x => x.Set(y => y.Text, "HR notified of approval"))
                               )
                           )
                           .Else(elseBranch => elseBranch
                               .StartWith<WriteLine>(activity => activity.Set(x => x.Text, "HR rejected the leave request."))
                               .Then(activity => activity
                                   .StartWith<WriteLine>(x => x.Set(y => y.Text, "Leave request rejected"))
                               )
                           )
                       )
                   )
                   .Else(elseBranch => elseBranch
                       .StartWith<WriteLine>(activity => activity.Set(x => x.Text, "Manager rejected the leave request."))
                       .Then(activity => activity
                           .StartWith<WriteLine>(x => x.Set(y => y.Text, "Leave request rejected"))
                       )
                   )
               );
    }
}
