namespace Utilities.Common.Security
{
    public interface IHashing
    {
        byte[] ComputeHash(byte[] password, byte[] salt);
        byte[] ComputeHash(object obj);
        string ComputeHash(string data);
    }
}