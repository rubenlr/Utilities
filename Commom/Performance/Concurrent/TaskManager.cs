using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using log4net;

namespace Utilities.Common.Performance.Concurrent
{
    public class TaskManagerConcurrent : IDisposable
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().GetType());

        private readonly Action _action;
        private readonly BlockingCollection<Task> _tasks = new BlockingCollection<Task>();

        public TaskManagerConcurrent(Action action)
        {
            _action = action;
        }

        private int _quantity;

        public void Start(int quantity)
        {
            _quantity += quantity;
            new Task(() => Parallel.For(0, quantity, i => Start())).Start();
        }

        public void Start(int quantity, TimeSpan intervalBetweenThreads)
        {
            _quantity += quantity;

            for (var i = 0; i < quantity; i++)
            {
                Start();
                Thread.Sleep(intervalBetweenThreads);
            }
        }

        public void Start(int quantity, int periodoMiliseconds)
        {
            _quantity += quantity;

            var interval = periodoMiliseconds / quantity;

            for (var i = 0; i < quantity; i++)
            {
                Start();
                Thread.Sleep(interval);
            }
        }

        private void Start()
        {
            var task = new Task(TaskRunner);
            task.Start();

            _tasks.Add(task);
        }

        private void TaskRunner()
        {
            _action();
        }

        public void WaitAll()
        {
            for (var i = 0; i < _quantity; i++)
            {
                while (_tasks.Count == 0)
                    Thread.Sleep(100);

                var task = _tasks.Take();

                while (!task.IsCompleted)
                    Thread.Sleep(100);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_tasks != null)
                    _tasks.Dispose();
            }
        }
    }
}
