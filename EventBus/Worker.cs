using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EventBus
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        private readonly IBus _bus;

        private static Random _random;

        public Worker(ILogger<Worker> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;
            _random = new Random();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                // await Task.Delay(1000, stoppingToken);

                var phoneNumber = _random.Next(1000, 9999);
                var message = RandomString(10);

                // await _bus.Publish<Sms>(
                //     new {PhoneNumber = $"+7800555{phoneNumber}", Message = $"hello, {message}"},
                //     stoppingToken
                // );

                await Task.Delay(1000, stoppingToken);

                //await _bus.SmsSent(DateTime.UtcNow);
            }
        }

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }
}