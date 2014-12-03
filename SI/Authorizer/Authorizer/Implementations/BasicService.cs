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
            var decryptRequest = TripleDESWrapper.Decrypt(message, _managerKey);
            var request = JsonConvert.DeserializeObject<ResponseForService>(decryptRequest);
            return true;
        }

        public void ProcessMessage(string message)
        {
            
        }
    }
}