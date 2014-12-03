using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json.Serialization;

namespace Authorizer.Implementations
{
    public static class TripleDESWrapper
    {
        private static readonly TripleDESCryptoServiceProvider _alg = new TripleDESCryptoServiceProvider();

        private static byte[] IV = {0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08};

        public static string Encrypt(string data, byte[] key)
        {
            var mStream = new MemoryStream();
            var cStream = new CryptoStream(mStream,
                new TripleDESCryptoServiceProvider().CreateEncryptor(key, IV),
                CryptoStreamMode.Write);

            byte[] toEncrypt = new ASCIIEncoding().GetBytes(data);

            cStream.Write(toEncrypt, 0, toEncrypt.Length);
            cStream.FlushFinalBlock();

            var ret = mStream.ToArray();

            cStream.Close();
            mStream.Close();

            return Convert.ToBase64String(ret);
        }

        public static string Decrypt(string encodedData, byte[] key)
        {
            var data = Convert.FromBase64String(encodedData);

            var msDecrypt = new MemoryStream(data);
            var csDecrypt = new CryptoStream(msDecrypt,
                new TripleDESCryptoServiceProvider().CreateDecryptor(key, IV),
                CryptoStreamMode.Read);
            var fromEncrypt = new byte[data.Length];
            csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);

            msDecrypt.Close();
            csDecrypt.Close();
            return new ASCIIEncoding().GetString(fromEncrypt);

        }
    }
}