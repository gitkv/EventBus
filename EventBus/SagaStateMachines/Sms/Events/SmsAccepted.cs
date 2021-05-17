using System;
using MassTransit;

namespace EventBus.SagaStateMachines.Sms.Events
{
    public interface SmsAccepted : CorrelatedBy<Guid>
    {
        string PhoneNumber { get; }
        string Message { get; }
    }
}