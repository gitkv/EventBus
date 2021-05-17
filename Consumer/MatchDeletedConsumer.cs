using System.Threading.Tasks;
using Common.Events.Match;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Consumer
{
    public class MatchDeletedConsumer : IConsumer<MatchDeleted>
    {
        ILogger<MatchDeletedConsumer> _logger;

        public MatchDeletedConsumer(ILogger<MatchDeletedConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<MatchDeleted> context)
        {
            _logger.LogInformation("MatchDeletedConsumer: {MatchId}", context.Message.MatchId);
            
            return Task.CompletedTask;
        }
    }
}