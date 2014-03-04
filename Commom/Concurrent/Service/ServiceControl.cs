using System.Collections.Generic;
using Utilities.Common.Concurrent.Interface;

namespace Utilities.Common.Concurrent.Service
{
    public class ServiceControl : IService
    {
        private readonly ICollection<IService> _services;

        private ServiceControl()
        {
            _services = new List<IService>();
        }

        public ServiceControl(params IService[] services)
            : this()
        {
            if (services != null)
            {
                foreach (var service in services)
                    _services.Add(service);
            }
        }

        public void Add(IService service)
        {
            _services.Add(service);
        }

        public void Remove(IService service)
        {
            _services.Remove(service);
        }

        public void Startup()
        {
            if (_services != null)
            {
                foreach (var service in _services)
                    service.Startup();
            }
        }

        public void RequestShutdown()
        {
            if (_services != null)
            {
                foreach (var service in _services)
                    service.RequestShutdown();
            }
        }

        public void Shutdown()
        {
            RequestShutdown();

            if (_services != null)
            {
                foreach (var service in _services)
                    service.Shutdown();
            }
        }
    }
}