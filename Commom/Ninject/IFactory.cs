using System;

namespace Utilities.Common.Ninject
{
    public interface IFactory : IDisposable
    {
        TInterface Create<TInterface>();
    }
}