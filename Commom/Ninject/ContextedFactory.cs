using Ninject.Syntax;

namespace Utilities.Common.Ninject
{
    public sealed class ContextedFactory : IContextedFactory
    {
        private readonly IResolutionRoot _kernel;

        public ContextedFactory(IResolutionRoot kernel)
        {
            _kernel = kernel;
        }

        public IFactory GetContext()
        {
            return new Factory(_kernel);
        }
    }
}