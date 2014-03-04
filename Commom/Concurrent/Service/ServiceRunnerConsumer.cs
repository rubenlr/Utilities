using System.Reflection;
using log4net;
using Utilities.Common.Concurrent.Interface;
using Utilities.Common.Concurrent.Service.Interface;

namespace Utilities.Common.Concurrent.Service
{
    public class ServiceRunnerConsumer<T> : ServiceRunner
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IConsumerRunner<T> _consumer;

        private class ConsumerRunner<TK> : IRunner
        {
            private readonly IConsumerRunner<TK> _producer;

            public ConsumerRunner(IConsumerRunner<TK> producer)
            {
                _producer = producer;
            }

            public bool RunInteration()
            {
                var item = _producer.ConsumeCollection.Take();

                Log.DebugFormat("Há {0} items na fila a serem processados.", _producer.ConsumeCollection.Count);

                try
                {
                    _producer.RunConsume(item);
                }
                catch
                {
                    _producer.ConsumeCollection.Add(item);
                    throw;
                }

                return true;
            }
        }

        public ServiceRunnerConsumer(ISettingsRunnable settings, IConsumerRunner<T> consumer, string threadName, bool autoStart = false)
            : base(settings, new ConsumerRunner<T>(consumer), threadName, autoStart)
        {
            _consumer = consumer;
        }

        protected override void InterationBegin()
        {
            base.InterationBegin();

            while (_consumer.ConsumeCollection.Count == 0)
                _consumer.ConsumeCollection.WaitOne();
        }
    }
}