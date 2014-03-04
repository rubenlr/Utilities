using Utilities.Common.Concurrent.Interface;
using Utilities.Common.Concurrent.Service.Interface;

namespace Utilities.Common.Concurrent.Service
{
    public class ServiceRunnerProducer<T> : ServiceRunner
    {
        protected readonly IProducerRunner<T> Producer;

        private class ProducerRunner : IRunner
        {
            private readonly IProducerRunner<T> _producer;

            public ProducerRunner(IProducerRunner<T> producer)
            {
                _producer = producer;
            }

            public bool RunInteration()
            {
                var result = _producer.RunProducer();

                if (result != null && result.Count > 0)
                {
                    _producer.ProducedCollection.AddRange(result);
                    return true;
                }

                return false;
            }
        }

        public ServiceRunnerProducer(ISettingsRunnable settings, IProducerRunner<T> runner, string threadName, bool autoStart = false)
            : base(settings, new ProducerRunner(runner), threadName, autoStart)
        {
            Producer = runner;
        }
    }
}