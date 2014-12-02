using System;
using System.Security.Cryptography;
using Authorizer.Interfaces;
using Authorizer.Models;
using Newtonsoft.Json;

namespace Authorizer.Implementations
{
    public class BasicService : IPrivateService
    {
        public string Name { get; set; }
        private readonly byte[] _managerKey;

        public BasicService(string name, IKeyManager keyManager)
        {
            Name = name;
            _managerKey = keyManager.Greet(this);
        }

        public bool InitialConnection(string message)
        {
            var des = new TripleDESCryptoServiceProvider();
            des.Key = _managerKey;
            var crypto = new TripleDESWrapper(des);

            var decryptRequest = crypto.Decrypt(message);
            var request = JsonConvert.DeserializeObject<ResponseForService>(decryptRequest);

            return true;
        }

        public void ProcessMessage(string message)
        {
            
        }
    }
}