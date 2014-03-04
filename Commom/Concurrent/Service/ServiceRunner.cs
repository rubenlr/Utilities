using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using log4net;
using Utilities.Common.Concurrent.Interface;
using Utilities.Common.Concurrent.Service.Interface;
using Utilities.Common.Performance;

namespace Utilities.Common.Concurrent.Service
{
    public delegate void ServiceRunnerInteration();
    public delegate void ServiceRunnerInterationError(Exception ex);

    public class ServiceRunner : IService, IRunner
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private const int Sleep = 100;

        private readonly ISettingsRunnable _settings;
        private readonly IRunner _runnableObject;

        private readonly TypeSyncronized<bool> _isRunning = new TypeSyncronized<bool>(false);
        private readonly string _threadName;
        public virtual bool IsRunning { get { return _isRunning.Value || CheckActiveForDependences(); } }

        public event ServiceRunnerInteration OnInterationBegins;
        public event ServiceRunnerInteration OnInterationSucess;
        public event ServiceRunnerInteration OnInterationNoJob;
        public event ServiceRunnerInterationError OnInterationError;
        public readonly ICollection<ServiceRunner> ServicesDependece = new List<ServiceRunner>();

        private Thread _thread;

        public ServiceRunner(ISettingsRunnable settings, IRunner runner, string threadName, bool autoStart = false)
        {
            _settings = settings;
            _runnableObject = runner;
            _threadName = threadName;

            if (autoStart)
                Start();
        }

        public ServiceRunner(ISettingsRunnable settings, IImportavel importador, string threadName, bool autoStart = false)
            : this(settings, new ImportadorService(importador), threadName, autoStart)
        {
        }

        private void Start()
        {
            if (!_isRunning.Value)
            {
                Log.InfoFormat("Starting {0} thread.", _threadName);

                _isRunning.Value = true;
                _thread = new Thread(ExecutRunner) { Name = _threadName };
                _thread.Start();

                Log.InfoFormat("Thread {0} started.", _threadName);
            }
            else
            {
                Log.InfoFormat("Thread {0} already started.", _threadName);
            }
        }

        public bool RunInteration()
        {
            if (_settings.Ativo)
                using (new Cronometro("Interação da thread {0}", _threadName))
                    return _runnableObject.RunInteration();

            return false;
        }

        private void ExecutRunner()
        {
            while (IsRunning)
            {
                try
                {
                    InterationBegin();

                    if (_settings.Ativo == false)
                    {
                        Log.DebugFormat("Thread {0} inativa.", _threadName);
                        Thread.Sleep(_settings.IntervaloInativo);
                        continue;
                    }

                    var hasDoneSomeJob = RunInteration();

                    if (hasDoneSomeJob)
                    {
                        InterationSucess();
                    }
                    else
                    {
                        InterationNoJob();
                        EsperarIntervaloSemServico();
                    }
                }
                catch (Exception ex)
                {
                    var message = String.Format("Thread [{0}] disparou um erro durante sua execução", _threadName);
                    Log.Error(message, ex);

                    InterationException(ex);

                    EsperarIntervaloSemServico();
                }
            }
        }

        private void EsperarIntervaloSemServico()
        {
            for (var tempo = 0; tempo < _settings.IntervaloSemServico && _isRunning.Value; tempo += Sleep)
                Thread.Sleep(Sleep);
        }

        private bool CheckActiveForDependences()
        {
            return ServicesDependece.Any(service => service.IsRunning);
        }

        protected virtual void InterationBegin()
        {
            if (OnInterationBegins != null)
                OnInterationBegins();
        }

        private void InterationSucess()
        {
            if (OnInterationSucess != null)
                OnInterationSucess();
        }

        private void InterationNoJob()
        {
            if (OnInterationNoJob != null)
                OnInterationNoJob();
        }

        private void InterationException(Exception ex)
        {
            if (OnInterationError != null)
                OnInterationError(ex);
        }

        public void Startup()
        {
            Start();
        }

        public void RequestShutdown()
        {
            _isRunning.Value = false;
            Log.InfoFormat("RequestStop {0} thread.", _thread.Name);
        }

        public void Shutdown()
        {
            RequestShutdown();

            Log.InfoFormat("WaitStop {0} thread.", _thread.Name);

            _thread.Join();
            Log.InfoFormat("Thread {0} has stopped.", _thread.Name);
        }
    }
}