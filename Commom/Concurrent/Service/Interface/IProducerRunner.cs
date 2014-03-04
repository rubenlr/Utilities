using System.Collections.Generic;
using Utilities.Common.Collections.Generic;

namespace Utilities.Common.Concurrent.Service.Interface
{
    public interface IProducerRunner<TProduce>
    {
        BlockCollectionNotifier<TProduce> ProducedCollection { get; }

        ICollection<TProduce> RunProducer();
    }
}