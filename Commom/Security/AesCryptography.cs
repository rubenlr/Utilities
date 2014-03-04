namespace Utilities.Common.Security
{
    public class Aes256Cryptography : ICryptography
    {
        private readonly ICryptographyKeyProvider _keyProvider;

        public Aes256Cryptography(ICryptographyKeyProvider keyProvider)
        {
            _keyProvider = keyProvider;
        }

        public string Encrypt(string data)
        {
            return AdvancedEncryptionStandard.Encrypt(data, _keyProvider.Key1, _keyProvider.Key2, AdvancedEncryptionStandard.AesCryptographyLevel.Aes256);
        }

        public string Decrypt(string encriptedData)
        {
            return AdvancedEncryptionStandard.Decrypt(encriptedData, _keyProvider.Key1, _keyProvider.Key2, AdvancedEncryptionStandard.AesCryptographyLevel.Aes256);
        }
    }
}
