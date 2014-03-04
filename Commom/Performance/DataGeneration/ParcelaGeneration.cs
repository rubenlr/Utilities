using System;
using System.Collections.Generic;
using Utilities.Common.Performance.DataGeneration.Models;

namespace Utilities.Common.Performance.DataGeneration
{
    public class ParcelaGeneration<T, TKCobranca>
        where T : IParcela, new()
        where TKCobranca : ICobranca, new()
    {
        private readonly Random _rand = new Random(1);
        private readonly DataGenerationSettings _settings;
        private readonly CobrancaGeneration<TKCobranca> _cobrancaGeneration;

        public ParcelaGeneration(DataGenerationSettings settings)
        {
            _settings = settings;
            _cobrancaGeneration = new CobrancaGeneration<TKCobranca>();
        }

        private T GetNew(IContrato contrato)
        {
            var parcela = new T
                              {
                                  Id = Guid.NewGuid(),
                                  Valor = DateTime.Now.Millisecond,
                                  UltimaAtualizacao = DateTime.Now.AddSeconds(-_rand.Next(120)),
                                  Cobrancas = new List<ICobranca>()
                              };


            foreach (var item in _cobrancaGeneration.GetNew(parcela, _settings.SubItens.Take()))
                parcela.Cobrancas.Add(item);

            return parcela;
        }

        public List<T> GetNew(IContrato contrato, int qtd)
        {
            var parcelas = new List<T>(qtd);

            for (var i = 0; i < qtd; i++)
                parcelas.Add(GetNew(contrato));

            return parcelas;
        }
    }
}
