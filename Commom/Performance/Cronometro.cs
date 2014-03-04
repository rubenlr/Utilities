using System;
using System.Diagnostics;
using log4net;

namespace Utilities.Common.Performance
{
    public sealed class Cronometro : IDisposable
    {
        private readonly Stopwatch _cronometro;
        private readonly int _level;
        private string _descricao;

        #region Construtores

        public Cronometro()
        {
            _cronometro = Stopwatch.StartNew();
        }

        public Cronometro(int level)
            : this()
        {
            _level = level;
        }

        public Cronometro(string descricao)
            : this()
        {
            _descricao = descricao;
        }

        public Cronometro(string descricao, params object[] parameters)
            : this(string.Format(descricao, parameters))
        {
        }

        #endregion

        public void AddDescricao(string format, params object[] parameters)
        {
            _descricao += " - " + string.Format(format, parameters);
        }

        /// <summary>
        /// Marcar a contagem de tempo e loga essa contagem com o nível indicado no callStack.
        /// </summary>
        /// <param name="level">Nível do callStack a ser logado. 0 = própria função que está chamando.</param>
        /// <returns>Tempo marcado em milisegundos</returns>
        public long Marcar(int level = 1)
        {
            return Marcar(string.Empty, level + 1);
        }

        public long MarcarFormat(string message, params object[] paramters)
        {
            return Marcar(string.Format(message, paramters), 1);
        }

        public long Marcar(string message, int level = 1)
        {
            var elapsedTime = _cronometro.ElapsedMilliseconds;

            var type = new StackTrace().GetFrame(level + 1).GetMethod().DeclaringType;
            var stack = new StackTrace().GetFrame(level + 1).GetMethod().Name;

            GetLog(type).InfoFormat(String.IsNullOrEmpty(message)
                                      ? @"Marcador de tempo da função {0} é {1}ms"
                                      : @"Marcador de tempo da função {0} é {1}ms - {2}", stack, elapsedTime, message);

            return elapsedTime;
        }

        private ILog GetLog(Type type)
        {
            return LogManager.GetLogger(type);
        }

        /// <summary>
        /// Finaliza a contagem de tempo e loga essa contagem com o nível indicado no callStack.
        /// </summary>
        /// <param name="level">Nível do callStack a ser logado. 0 = própria função que está chamando.</param>
        /// <returns>Tempo total depois de finalizado</returns>
        public long Finalizar(int level = 1)
        {
            var type = new StackTrace().GetFrame(level + 1).GetMethod().DeclaringType;
            var stack = new StackTrace().GetFrame(level + 1).GetMethod().Name;

            if (_cronometro.IsRunning)
            {
                _cronometro.Stop();

                if (_descricao != null)
                {
                    GetLog(type).InfoFormat(@"Consumo de tempo <{2}> <<descrição: {1}>> é {0}ms",
                                           _cronometro.ElapsedMilliseconds,
                                           _descricao,
                                           stack);
                }
                else
                {
                    GetLog(type).InfoFormat(@"Consumo de tempo é {0}ms", _cronometro.ElapsedMilliseconds);
                }
            }

            return _cronometro.ElapsedMilliseconds;
        }

        public void Dispose()
        {
            Finalizar(1 + _level);
        }
    }
}
