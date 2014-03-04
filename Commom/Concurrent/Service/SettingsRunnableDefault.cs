using System;
using Utilities.Common.Concurrent.Interface;
using Utilities.Common.Settings;

namespace Utilities.Common.Concurrent.Service
{
    public abstract class SettingsRunnableDefault : ISettingsRunnable
    {
        private readonly IAppConfig _appConfig;
        private readonly string _ativo;
        private readonly string _intervaloSemServico;
        private readonly string _intervaloInativo;

        protected SettingsRunnableDefault(IAppConfig appConfig,
                                          string ativo,
                                          string intervaloSemServico,
                                          string intervaloInativo)
        {
            _appConfig = appConfig;
            _ativo = ativo;
            _intervaloSemServico = intervaloSemServico;
            _intervaloInativo = intervaloInativo;
        }

        public virtual bool Ativo
        {
            get { return _appConfig.GetBoolean(_ativo, false); }
        }

        public virtual int IntervaloSemServico
        {
            get
            {
                var intervalo = _appConfig.GetInt32(_intervaloSemServico, -1);

                if (intervalo > 0)
                    return (int)TimeSpan.FromSeconds(intervalo).TotalMilliseconds;

                return (int)TimeSpan.FromSeconds(_appConfig.GetInt32("intervalo-sem-servico-default", 60)).TotalMilliseconds;
            }
        }

        public virtual int IntervaloInativo
        {
            get
            {
                var intervalo = _appConfig.GetInt32(_intervaloInativo, -1);

                if (intervalo > 0)
                    return (int)TimeSpan.FromSeconds(intervalo).TotalMilliseconds;

                return (int)TimeSpan.FromSeconds(_appConfig.GetInt32("intervalo-inativo-default", 60)).TotalMilliseconds;
            }
        }
    }
}