using System;
using System.Threading.Tasks;
using MassTransit;

namespace Common.Command
{
    public abstract class MatchDeletedCommand : EventBusCommand
    {

        protected override string GetQueueName()
        {
            return "match.deleted";
        }

        protected MatchDeletedCommand(IBus bus) : base(bus)
        {
        }
    }
}