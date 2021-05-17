using System;
using Automatonymous;

namespace EventBus.SagaStateMachines.Sms
{
    public class SmsState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        
        public int CurrentState { get; set; }
        
        public string? PhoneNumber { get; set; }
    }
}