using System;
using Utilities.Common.Performance.Mensure;

namespace Utilities.Common.Performance.Concurrent.StressTest
{
    public class CrudStressRunner<T> where T : class
    {
        private readonly ICrudDataHandleStressable<T> _crudGuidKey;
        private readonly CrudStressData<T> _data;

        public CrudStressRunner(ICrudDataHandleStressable<T> crudGuidKey, CrudStressData<T> data)
        {
            _crudGuidKey = crudGuidKey;
            _data = data;
        }

        public void Run()
        {
            var tasksInsertFull = new TaskManagerConcurrent(InserirItemDumb);
            var tasksInsert = new TaskManagerConcurrent(InserirItem);
            var taskUpdates = new TaskManagerConcurrent(UpdateItems);
            var taskDelete = new TaskManagerConcurrent(DeleteItems);
            var taskSelects = new TaskManagerConcurrent(SelectItems);

            tasksInsertFull.Start(_data.InsertsDumb.Count / 1000);

            tasksInsert.Start(_data.InsertsToManipulate.Count);
            tasksInsert.WaitAll();

            taskUpdates.Start(_data.Updates.Count);
            taskSelects.Start(_data.Selects.Count);
            taskDelete.Start(_data.Deletes.Count);

            tasksInsertFull.WaitAll();
            taskUpdates.WaitAll();
            taskSelects.WaitAll();
            taskDelete.WaitAll();
        }

        void InserirItemDumb()
        {
            var items = _data.InsertsDumb.Take(1000);

            using (var cron = new CronometroPerformance("insert-dumb"))
            {
                foreach (var item in items)
                {
                    try
                    {
                        _crudGuidKey.Insert(item);
                    }
                    catch (Exception ex)
                    {
                        cron.MensagemErro = ex.Message;
                    }
                }
            }
        }

        void InserirItem()
        {
            var item = _data.InsertsToManipulate.Take();

            using (var cron = new CronometroPerformance("insert-item"))
            {
                try
                {
                    _crudGuidKey.Insert(item);
                }
                catch (Exception ex)
                {
                    cron.MensagemErro = ex.Message;
                }
            }
        }

        void UpdateItems()
        {
            var item = _data.Updates.Take();
            
            using (var cron = new CronometroPerformance("update"))
            {
                try
                {
                    _crudGuidKey.Update(item);
                }
                catch (Exception ex)
                {
                    cron.MensagemErro = ex.Message;
                }
            }
        }

        void DeleteItems()
        {
            var item = _data.Deletes.Take();

            using (var cron = new CronometroPerformance("delete"))
            {
                try
                {
                    _crudGuidKey.Delete(item);
                }
                catch (Exception ex)
                {
                    cron.MensagemErro = ex.Message;
                }
            }
        }

        void SelectItems()
        {
            var item = _data.Selects.Take();
            
            using (var cron = new CronometroPerformance("select-unitario"))
            {
                try
                {
                    var resp = _crudGuidKey.Select(item);

                    if (resp == null)
                        cron.MensagemErro = string.Format("resp = null para {0}", item);
                    else if (!resp.Equals(item))
                        cron.MensagemErro = string.Format("items diferentes. {0} != {1}", resp, item);
                }
                catch (Exception ex)
                {
                    cron.MensagemErro = ex.Message;
                }
            }
        }
    }
}
