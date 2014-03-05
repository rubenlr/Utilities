using System;
using System.Collections.Generic;
using Ninject.Activation;
using Ninject.Activation.Blocks;
using Ninject.Parameters;
using Ninject.Planning.Bindings;
using Ninject.Syntax;

namespace Utilities.DataAccess
{
    public class NinjectUnitOfWork : ActivationBlock
    {
        public NinjectUnitOfWork(IResolutionRoot parent) :
            base(parent)
        {
        }

        public override IRequest CreateRequest(Type service, Func<IBindingMetadata, bool> constraint, IEnumerable<IParameter> parameters, bool isOptional, bool isUnique)
        {
            return Parent.CreateRequest(service, constraint, parameters, isOptional, isUnique);
        }
    }
}