namespace Utilities.Common.Performance.Concurrent.StressTest
{
    public class StressTestSettings
    {
        public virtual int TotalDumbInserts { get { return 100000; } }
        public virtual int TotalInsertsToManipulate { get { return 5000; } }
        public virtual int Updates { get { return (int)(TotalInsertsToManipulate * 0.40f); } }
        public virtual int Deletes { get { return (int)(TotalInsertsToManipulate * 0.60f); } }
        public virtual int Selects { get { return TotalInsertsToManipulate * 0; } }
        public virtual int SelectsInterval { get { return (int)(TotalDumbInserts * 0.10f); } }
        public virtual int TotalCiclos { get { return 100; } }

        public override string ToString()
        {
            return string.Format("dumb inserts {0}; inserts {1}; updates {2}; deletes {3}; selects {4}",
                                 TotalDumbInserts, TotalInsertsToManipulate, Updates, Deletes, Selects);
        }
    }
}