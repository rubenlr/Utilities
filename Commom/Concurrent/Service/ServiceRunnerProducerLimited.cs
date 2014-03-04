using System.Threading;
using Utilities.Common.Concurrent.Service.Interface;

namespace Utilities.Common.Concurrent.Service
{
    public class ServiceRunnerProducerLimited<T> : ServiceRunnerProducer<T>
    {
        private readonly ISettingsProducerLimited _settings;

        public ServiceRunnerProducerLimited(ISettingsProducerLimited settings, IProducerRunner<T> runner, string threadName, bool autoStart = false)
            : base(settings, runner, threadName, autoStart)
        {
            _settings = settings;
        }

        protected override void InterationBegin()
        {
            base.InterationBegin();

            while (Producer.ProducedCollection.Count > _settings.ProducerLimit)
                Thread.Sleep(100);
        }
    }
}