namespace Utilities.Common.Security
{
    public interface ICryptographyKeyProvider
    {
        byte[] Key1 { get; }
        byte[] Key2 { get; }
        void SaveKeyDono(string text);
        void SaveKeyDiretor(string text);
    }
}