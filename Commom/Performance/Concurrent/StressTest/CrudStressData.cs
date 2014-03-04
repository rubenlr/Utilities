
namespace Utilities.Common.Performance.Concurrent.StressTest
{
    public class CrudStressData<T>
    {
        private readonly StressTestSettings _settings;
        private readonly IDataGenerationProvider<T> _dataProvider;

        public CrudStressData(StressTestSettings settings, IDataGenerationProvider<T> dataGenerationProvider)
        {
            _settings = settings;
            _dataProvider = dataGenerationProvider;

            Generate();
        }

        private void Generate()
        {
            using (new Cronometro("Gerando TotalDumbInserts {0}", _settings.TotalDumbInserts))
                InsertsDumb = new StressTestDataCollection<T>(_dataProvider.GetData(_settings.TotalDumbInserts));

            StressTestDataCollection<T> data;

            using (new Cronometro("Gerando TotalInsertsToManipulate {0}", _settings.TotalInsertsToManipulate))
                data = new StressTestDataCollection<T>(_dataProvider.GetData(_settings.TotalInsertsToManipulate));

            InsertsToManipulate = new StressTestDataCollection<T>(data);

            Updates = new StressTestDataCollection<T>(data.Take(_settings.Updates));
            Deletes = new StressTestDataCollection<T>(data.Take(_settings.Deletes));
            Selects = new StressTestDataCollection<T>(data.Take(_settings.Selects));
        }

        public StressTestDataCollection<T> InsertsDumb { get; private set; }
        public StressTestDataCollection<T> InsertsToManipulate { get; private set; }
        public StressTestDataCollection<T> Updates { get; private set; }
        public StressTestDataCollection<T> Deletes { get; private set; }
        public StressTestDataCollection<T> Selects { get; private set; } 
    }
}
