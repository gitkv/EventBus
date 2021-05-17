using System.Threading.Tasks;
using Common.Events.Match;
using GreenPipes;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using Microsoft.Extensions.Logging;

namespace Consumer
{
    public class MatchDeletedConsumerDefinition : ConsumerDefinition<MatchDeletedConsumer>
    {
        public MatchDeletedConsumerDefinition(ILogger<MatchDeletedConsumer> logger)
        {
            // override the default endpoint name
            EndpointName = "match.deleted";

            // limit the number of messages consumed concurrently
            // this applies to the consumer only, not the endpoint
            ConcurrentMessageLimit = 8;
        }

        protected override void ConfigureConsumer(
            IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<MatchDeletedConsumer> consumerConfigurator
        )
        {
            // configure message retry with millisecond intervals
            endpointConfigurator.UseMessageRetry(r => r.Intervals(100, 200, 500, 800, 1000));

            // use the outbox to prevent duplicate events from being published
            endpointConfigurator.UseInMemoryOutbox();
        }
    }
}