using System;
using System.Reflection;
using EventBus.SagaStateMachines.Sms;
using MassTransit;
using MassTransit.Saga;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EventBus
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(configuration =>
                    {
                        // configuration.AddSaga<SmsState>();
                        // configuration.AddConsumer<SendSmsConsumer>();

                        // configuration
                        //     .AddSagaStateMachine<SmsStateMachine, SmsState>()
                        //     .InMemoryRepository()
                        //     .Endpoint(endpoint => { endpoint.Name = "sms"; });

                        
                        // configuration.AddSagaStateMachine(typeof(SmsState), typeof(SmsStateDefinition));
                        configuration.AddSagas(Assembly.GetExecutingAssembly());

                        configuration.UsingRabbitMq((context, rabbitConfigurator) =>
                        {
                            rabbitConfigurator.ConfigureEndpoints(context);
                            rabbitConfigurator.Host(new Uri($"amqp://localhost:5672"),
                                host =>
                                {
                                    host.Username("guest");
                                    host.Password("guest");
                                });

                            // rabbitConfigurator.ReceiveEndpoint("sms",
                            //     endpoint =>
                            //     {
                            //         endpoint.StateMachineSaga(
                            //             new SmsStateMachine(),
                            //             new InMemorySagaRepository<SmsState>()
                            //         );
                            //     }
                            // );
                        });
                    });

                    services.AddMassTransitHostedService(true);

                    services.AddHostedService<Worker>();
                });
    }
}

// using MassTransit;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;
//
// namespace EventBus
// {
//     public class Program
//     {
//         public static void Main(string[] args)
//         {
//             CreateHostBuilder(args).Build().Run();
//         }
//
//         public static IHostBuilder CreateHostBuilder(string[] args) =>
//             Host.CreateDefaultBuilder(args)
//                 .ConfigureServices((hostContext, services) =>
//                 {
//                     services.AddMassTransit(configuration =>
//                     {
//                         configuration.AddConsumer<SendSmsConsumer>();
//                         configuration.UsingRabbitMq((context, configurator) =>
//                         {
//                             configurator.ConfigureEndpoints(context);
//                             configurator.Host("localhost",
//                                 "/",
//                                 host =>
//                                 {
//                                     host.Username("guest");
//                                     host.Password("guest");
//                                 });
//                         });
//                     });
//
//                     services.AddMassTransitHostedService(true);
//
//                     services.AddHostedService<Worker>();
//                 });
//     }
// }