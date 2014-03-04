namespace Utilities.Common.Concurrent.Interface
{
    public interface ISettingsRunnable
    {
        bool Ativo { get; }
        int IntervaloSemServico { get; }
        int IntervaloInativo { get; }
    }
}