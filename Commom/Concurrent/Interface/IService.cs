
namespace Utilities.Common.Concurrent.Interface
{
    public interface IService
    {
        void Startup();
        void RequestShutdown();
        void Shutdown();
    }
}
