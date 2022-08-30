using Cryptung;

namespace Cryptung
{
    public static class CryptungServiceFactory
    {
        public static ICryptungService Create(string encryptionKey)
        {
            return new CryptungService(encryptionKey);
        }
    }
}