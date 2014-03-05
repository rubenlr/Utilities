using Ninject;
using Ninject.Syntax;

namespace Utilities.DataAccess
{
    public static class NinjectExtensions
    {
        public static IBindingNamedWithOrOnSyntax<T> InUnitOfWorkScope<T>(this IBindingWhenInNamedWithOrOnSyntax<T> self)
        {
            return self.InScope(x => self.Kernel.Get<NinjectUnitOfWork>());
        }
    }
}