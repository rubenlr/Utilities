using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Utilities.Common.Collections.Generic
{
    public class CachedCollectionHandler<T>
    {
        public delegate IList<T> DataProvider();

        private readonly DataProvider _dataProvider;
        private readonly IExpiredTimeSettings _expiredPeriod;
        private IList<T> _collection;
        private DateTime _lastDataRetrivier;

        public CachedCollectionHandler(DataProvider dataProvider, IExpiredTimeSettings expiredPeriod)
        {
            _dataProvider = dataProvider;
            _expiredPeriod = expiredPeriod;
            _lastDataRetrivier = DateTime.Now;
        }

        private void AtualizaSeForNecessario()
        {
            if (_collection == null || DateTime.Now > (_lastDataRetrivier + _expiredPeriod.ExpirtedPeriod))
            {
                _lastDataRetrivier = DateTime.Now;

                var data = _dataProvider();

                if (data != null)
                    _collection = new ReadOnlyCollection<T>(data);
                else
                    _collection = new List<T>();
            }
        }

        public IList<T> CachedCollection
        {
            get
            {
                AtualizaSeForNecessario();

                return _collection;
            }
        }
    }
}