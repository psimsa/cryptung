using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Cryptung
{
    internal class CryptungService : ICryptungService
    {
        private readonly byte[] _encryptionKey;

        internal CryptungService(string encryptionKey)
        {
            using var sha = SHA256.Create();
            _encryptionKey = GetHash(sha, encryptionKey);
        }

        public string Encrypt(string input)
        {
            var (iv, encryptor) = GetEncryptor();
            var inputData = Encoding.UTF8.GetBytes(input);
            var encryptedInput = Process(inputData, encryptor);
            var encryptionObject = new CryptungObject(iv, encryptedInput);
            return encryptionObject.ToBase64();
        }

        public string Decrypt(string input)
        {
            var encryptionObject = CryptungObject.FromBase64(input);
            var decryptor = GetDecryptor(encryptionObject._iv);
            var decryptedInput = Process(encryptionObject._value, decryptor);
            return Encoding.UTF8.GetString(decryptedInput);
        }

        public string Recrypt(string input, string oldKey)
        {
            var oldKeyCryptungService = new CryptungService(oldKey);
            var decryptedValue = oldKeyCryptungService.Decrypt(input);
            return Encrypt(decryptedValue);
        }

        private static byte[] Process(byte[] inputData, ICryptoTransform transformer)
        {
            using var output = new MemoryStream();
            using var cryptoStream = new CryptoStream(output, transformer, CryptoStreamMode.Write);
            using var input = new MemoryStream(inputData);
            input.CopyTo(cryptoStream);
            cryptoStream.FlushFinalBlock();

            return output.ToArray();
        }

        private Aes GetEncryptionProvider(byte[]? iv = null)
        {
            var enc = Aes.Create();
            enc.Key = _encryptionKey;
            if (iv != null)
                enc.IV = iv;
            return enc;
        }

        private (byte[] iv, ICryptoTransform) GetEncryptor()
        {
            var aes = GetEncryptionProvider();
            return (aes.IV, aes.CreateEncryptor());
        }

        private ICryptoTransform GetDecryptor(byte[] iv)
        {
            var aes = GetEncryptionProvider(iv);
            return aes.CreateDecryptor();
        }

        private static byte[] GetHash(HashAlgorithm hashAlgorithm, string input) =>
            hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
    }

    internal readonly struct CryptungObject
    {
        internal byte[] _iv { get; }
        internal byte[] _value { get; }

        public CryptungObject(byte[] iv, byte[] value)
        {
            _iv = iv;
            _value = value;
        }

        internal static CryptungObject FromBase64(string base64)
        {
            var bytes = Convert.FromBase64String(base64);
            var iv = new byte[16];
            var value = new byte[bytes.Length - 16];

            Array.Copy(bytes, iv, 16);
            Array.Copy(bytes, 16, value, 0, value.Length);

            return new CryptungObject(iv, value);
        }

        internal string ToBase64()
        {
            var bytes = new byte[_iv.Length + _value.Length];
            Array.Copy(_iv, 0, bytes, 0, _iv.Length);
            Array.Copy(_value, 0, bytes, _iv.Length, _value.Length);
            return Convert.ToBase64String(bytes);
        }
    }
}