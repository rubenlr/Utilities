using System;
using Ninject;
using Ninject.Activation.Blocks;
using Ninject.Syntax;

namespace Utilities.Common.Ninject
{
    public sealed class Factory : IFactory, IDisposable
    {
        private readonly ActivationBlock _block;

        public Factory(IResolutionRoot kernel)
        {
            _block = new ActivationBlock(kernel);
        }

        public TInterface Create<TInterface>()
        {
            return _block.Get<TInterface>();
        }

        public void Dispose()
        {
            _block.Dispose();
        }
    }
}