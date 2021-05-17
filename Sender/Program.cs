using System;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Sender
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(configuration =>
                    {
                        configuration.UsingRabbitMq((context, rabbitConfigurator) =>
                        {
                            rabbitConfigurator.ConfigureEndpoints(context);
                            rabbitConfigurator.Host(new Uri($"amqp://localhost:5672"),
                                host =>
                                {
                                    host.Username("guest");
                                    host.Password("guest");
                                });
                        });
                    });

                    services.AddMassTransitHostedService(true);

                    services.AddHostedService<Worker>();
                });
        }
    }
}