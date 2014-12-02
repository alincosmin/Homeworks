using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Authorizer.Implementations
{
    public class TripleDESWrapper
    {
        private readonly TripleDESCryptoServiceProvider _alg;

        public TripleDESWrapper(TripleDESCryptoServiceProvider des)
        {
            _alg = des;
        }

        public string Encrypt(string data)
        {
            var key = _alg.Key;
            var IV = _alg.IV;

            try
            {
                var mStream = new MemoryStream();
                var cStream = new CryptoStream(mStream,
                    new TripleDESCryptoServiceProvider().CreateEncryptor(key, IV),
                    CryptoStreamMode.Write);

                byte[] toEncrypt = new UTF8Encoding().GetBytes(data);

                cStream.Write(toEncrypt, 0, toEncrypt.Length);
                cStream.FlushFinalBlock();

                var ret = mStream.ToArray();

                cStream.Close();
                mStream.Close();

                return new UTF8Encoding().GetString(ret);
            }

            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                return null;
            }
        }

        public string Decrypt(string encodedData)
        {
            var key = _alg.Key;
            var IV = _alg.IV;
            var data = new UTF8Encoding().GetBytes(encodedData);

            try
            {
                var msDecrypt = new MemoryStream(data);
                var csDecrypt = new CryptoStream(msDecrypt,
                    new TripleDESCryptoServiceProvider().CreateDecryptor(key, IV),
                    CryptoStreamMode.Read);
                var fromEncrypt = new byte[data.Length];
                csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);
                return new UTF8Encoding().GetString(fromEncrypt);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                return null;
            }
        }
    }
}