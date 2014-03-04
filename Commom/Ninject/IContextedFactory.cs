
namespace Utilities.Common.Ninject
{
    public interface IContextedFactory
    {
        IFactory GetContext();
    }
}