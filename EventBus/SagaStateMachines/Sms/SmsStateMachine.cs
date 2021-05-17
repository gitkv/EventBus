using Automatonymous;
using EventBus.SagaStateMachines.Sms.Events;
using MassTransit;

// ReSharper disable MemberCanBePrivate.Global
namespace EventBus.SagaStateMachines.Sms
{
    public class SmsStateMachine : MassTransitStateMachine<SmsState>
    {
        public Event<SmsSubmit> SmsSubmit { get; private set; }

        public Event<SmsAccepted> SmsAccepted { get; private set; }

        public State Submitted { get; private set; }

        public State Accepted { get; private set; }

        public SmsStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Event(() => SmsSubmit);

            Initially(
                When(SmsSubmit)
                    .Send(context => new SmsState())
                    // .SendAsync(settings.AccountServiceAddress, context => context.Init<UpdateAccountHistory>(new { OrderId = context.Instance.CorrelationId }))
                    .Then(x => x.Instance.PhoneNumber = x.Data.PhoneNumber)
                    .TransitionTo(Submitted),
                When(SmsAccepted)
                    .TransitionTo(Accepted)
            );

            During(
                Submitted,
                When(SmsAccepted)
                    .TransitionTo(Accepted)
            );

            During(
                Accepted,
                Ignore(SmsSubmit)
            );

            // SetCompletedWhenFinalized();
        }
    }
}