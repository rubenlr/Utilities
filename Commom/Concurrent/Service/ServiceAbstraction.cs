using Utilities.Common.Concurrent.Interface;

namespace Utilities.Common.Concurrent.Service
{
    public abstract class ServiceAbstraction : IService
    {
        protected IService Service;

        public ServiceAbstraction(IService service)
        {
            Service = service;
        }

        public void Startup()
        {
            Service.Startup();
        }

        public void RequestShutdown()
        {
            Service.RequestShutdown();
        }

        public void Shutdown()
        {
            Service.Shutdown();
        }
    }
}
