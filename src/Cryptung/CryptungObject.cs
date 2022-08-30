using System;

namespace Cryptung
{
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