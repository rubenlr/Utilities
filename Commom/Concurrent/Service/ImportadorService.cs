using Utilities.Common.Concurrent.Interface;
using Utilities.Common.Concurrent.Service.Interface;

namespace Utilities.Common.Concurrent.Service
{
    public class ImportadorService : IRunner
    {
        private readonly IImportavel _importacao;

        public ImportadorService(IImportavel importacao)
        {
            _importacao = importacao;
        }

        public bool RunInteration()
        {
            return _importacao.Importar();
        }
    }
}
