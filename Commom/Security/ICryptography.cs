namespace Utilities.Common.Security
{
    public interface ICryptography
    {
        string Encrypt(string data);
        string Decrypt(string encriptedData);
    }
}