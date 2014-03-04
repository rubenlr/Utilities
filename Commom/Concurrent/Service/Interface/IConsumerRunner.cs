using Utilities.Common.Collections.Generic;

namespace Utilities.Common.Concurrent.Service.Interface
{
    public interface IConsumerRunner<TConsume>
    {
        BlockCollectionNotifier<TConsume> ConsumeCollection { get; }

        void RunConsume(TConsume item);
    }
}
