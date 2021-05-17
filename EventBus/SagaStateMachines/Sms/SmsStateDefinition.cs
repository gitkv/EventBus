using EventBus.SagaStateMachines.Sms.Events;
using GreenPipes;
using MassTransit;
using MassTransit.Definition;

namespace EventBus.SagaStateMachines.Sms
{
    public class SmsStateDefinition : SagaDefinition<SmsState>
    {
        public SmsStateDefinition()
        {
            Endpoint(endpoint => { endpoint.Name = "sms"; });
        }

        protected override void ConfigureSaga(IReceiveEndpointConfigurator endpointConfigurator, ISagaConfigurator<SmsState> sagaConfigurator)
        {
            var partition = endpointConfigurator.CreatePartitioner(16);

            sagaConfigurator.Message<SmsSubmit>(x => x.UsePartitioner(partition, m => m.Message.CorrelationId));
            sagaConfigurator.Message<SmsAccepted>(x => x.UsePartitioner(partition, m => m.Message.CorrelationId));
            // sagaConfigurator.Message<OrderCanceled>(x => x.UsePartitioner(partition, m => m.Message.CorrelationId));
            sagaConfigurator.UseInMemoryOutbox();
        }
    }
}