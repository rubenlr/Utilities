using Ninject;

namespace Utilities.Common
{
    public interface INinjectKernelProvider
    {
        StandardKernel Kernel { get; }
    }
}