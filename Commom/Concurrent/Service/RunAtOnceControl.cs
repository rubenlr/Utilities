using System;
using System.Collections.Generic;
using System.Reflection;
using log4net;
using Utilities.Common.Concurrent.Interface;

namespace Utilities.Common.Concurrent.Service
{
    public class RunAtOnceControl : IRunner
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().GetType());
        private readonly ICollection<IRunner> _runners;

        public RunAtOnceControl()
        {
            _runners = new List<IRunner>();
        }

        public RunAtOnceControl(params IRunner[] services)
            : this()
        {
            if (services != null)
            {
                foreach (var service in services)
                    Add(service);
            }
        }

        public bool RunInteration()
        {
            foreach (var runner in _runners)
            {
                try
                {
                    var result = runner.RunInteration();

                    if (result)
                        return true;
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                }
            }

            return false;
        }

        public void Add(IRunner service)
        {
            _runners.Add(service);
        }

        public void Remove(IRunner service)
        {
            _runners.Remove(service);
        }
    }
}