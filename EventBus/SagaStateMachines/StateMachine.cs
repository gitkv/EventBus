// using Automatonymous;
// using EventBus.SagaStateMachines.Sms;
//
// namespace EventBus.SagaStateMachines
// {
//     public sealed partial class StateMachine
//     {
//         public State AwaitingTaskCreated { get; set; }
//         public State AwaitingTaskTakedToWork { get; set; }
//         public State AwaitingDecisionAboutTask { get; set; }
//         public State Approved { get; set; }
//         public State Rejected { get; set; }
//
//         public Event<IStartWorkflowCommand> StartWorkflowCommandReceived { get; set; }
//         public Event<TaskCreatedNotification> TaskCreated { get; set; }
//         public Event<TaskTakedToWorkNotification> TaskTakedToWork { get; set; }
//         public Event<TaskApprovedNotification> TaskApproved { get; set; }
//         public Event<TaskDeclinedNotification> TaskRejected { get; set; }
//
//         private void BuildStateMachine()
//         {
//             InstanceState(x => x.CurrentState);
//             Event(() => StartWorkflowCommandReceived, x => x.CorrelateById(ctx =>
//                     ctx.Message.CorrelationId)
//                 .SelectId(context => context.Message.CorrelationId));
//             Event(() => TaskCreated, x => x.CorrelateById(ctx =>
//                 ctx.Message.CorrelationId));
//             Event(() => TaskTakedToWork, x => x.CorrelateById(ctx =>
//                 ctx.Message.CorrelationId));
//             Event(() => TaskRejected, x => x.CorrelateById(ctx =>
//                 ctx.Message.CorrelationId));
//             Event(() => TaskApproved, x => x.CorrelateById(ctx =>
//                 ctx.Message.CorrelationId));
//         }
//
//
//         // public State Submitted { get; private set; }
//         //
//         // public State Accepted { get; private set; }
//         //
//         // public State SmsAccepted { get; private set; }
//         //
//         // public StateMachine()
//         // {
//         //     // InstanceState(x => x.CurrentState, Submitted, Accepted);
//         //     
//         //     Initially(
//         //         When(SendSms)
//         //             .Then(x => x.Instance. = x.Data.OrderDate)
//         //             .TransitionTo(Submitted),
//         //         When(SmsAccepted)
//         //             .TransitionTo(Accepted));
//         //
//         //     During(Submitted,
//         //         When(SmsAccepted)
//         //             .TransitionTo(Accepted));
//         //
//         //     During(Accepted,
//         //         When(SendSms)
//         //             .Then(x => x.Instance.OrderDate = x.Data.OrderDate));
//         // }
//     }
// }