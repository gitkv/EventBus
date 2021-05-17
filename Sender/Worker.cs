using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Events.Match;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Sender
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        private readonly IBus _bus;

        public Worker(ILogger<Worker> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var matchId = Guid.NewGuid();
                var commandEndpoint = await _bus.GetSendEndpoint(new Uri("queue:match.deleted"));
                await commandEndpoint.Send<MatchDeleted>(
                    new {MathId = matchId},
                    stoppingToken
                );

                _logger.LogInformation($"Send MatchDeleted command, matchId: {matchId}");

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}