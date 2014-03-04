using Utilities.Common.Concurrent.Interface;

namespace Utilities.Common.Concurrent.Service.Interface
{
    public interface ISettingsProducerLimited : ISettingsRunnable
    {
        int ProducerLimit { get; }
    }
}