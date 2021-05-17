using System;
using System.Threading.Tasks;
using MassTransit;

namespace Common.Command
{
    public abstract class EventBusCommand
    {
        private readonly IBus _bus;

        protected EventBusCommand(IBus bus)
        {
            _bus = bus;
        }

        protected abstract string GetQueueName();

        private Uri GetUri() => new ($"queue:{GetQueueName()}");
        
        public async Task Send<T>()
        {
            var commandEndpoint = await _bus.GetSendEndpoint(GetUri());
            // await commandEndpoint.Send<T>(
            //     new {MathId = matchId}
            // );
        }
    }
}