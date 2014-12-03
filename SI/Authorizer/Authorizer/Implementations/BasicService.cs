using System;
using System.Collections.Generic;
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
        private Dictionary<IClient, LifeSpanKeyPair> Keys; 

        public BasicService(string name, IKeyManager keyManager)
        {
            Name = name;
            _managerKey = keyManager.Greet(this);
            Keys = new Dictionary<IClient, LifeSpanKeyPair>();
        }

        public bool InitialConnection(string message, IClient client)
        {
            var decryptRequest = TripleDESWrapper.Decrypt(message, _managerKey);
            var request = JsonConvert.DeserializeObject<ResponseForService>(decryptRequest);
            var expirationDate = DateTime.Now.Add(request.SessionLifeSpan);
            
            if(expirationDate.CompareTo(DateTime.Now) < 0) return false;
            
            var pair = new LifeSpanKeyPair()
            {
                ExpirationDate = expirationDate,
                Key = request.Key
            };

            Keys.Add(client, pair);

            return true;
        }

        public bool ProcessMessage(string message, IClient client)
        {
            if (Keys[client].ExpirationDate.CompareTo(DateTime.Now) < 0) return false;

            var decryptedMessage = TripleDESWrapper.Decrypt(message, Keys[client].Key);

            Console.WriteLine("Service: Received [{0}] as [{1}]", message, decryptedMessage);

            return true;
        }
    }
}