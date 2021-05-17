using System;
using MassTransit;

namespace EventBus.SagaStateMachines.Sms.Events
{
    public interface SmsSubmit : CorrelatedBy<Guid>
    {
        string PhoneNumber { get; }
        string Message { get; }
    }
}