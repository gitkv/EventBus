using System;
using System.Runtime.CompilerServices;
using MassTransit;
using MassTransit.Topology.Topologies;

namespace Common.Events.Match
{
    public interface MatchDeleted : CorrelatedBy<Guid>
    {
        public Guid MatchId { get; }
        
        [ModuleInitializer]
        internal static void Init() => GlobalTopology.Send.UseCorrelationId<MatchDeleted>(x => x.MatchId);
    }
}