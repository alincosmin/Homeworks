using System;
using System.Collections.Generic;
using Authorizer.Interfaces;
using Authorizer.Models;
using Newtonsoft.Json;

namespace Authorizer.Implementations
{
    public class BasicKeyManager : IKeyManager
    {
        public TimeSpan KeyLifeSpan { get; set; }
        private readonly IDictionary<string, string> _keys = new Dictionary<string, string>();

        public string GetInitialKey(string identity)
        {
            Console.WriteLine("Received request for initial connection. Will give secret key.");
            var key = "suchSecretKey";
            _keys.Add(identity, key);

            return key;
        }

        public bool GetKeyForService(string message, out string response)
        {
            response = "";
            var request = JsonConvert.DeserializeObject<ClientRequest>(message);

            if(!_keys.ContainsKey(request.ClientIdentity)) return false;

            var key = "somethingSomething";

            var clientResponse = new ResponseForClient
            {
                SessionLifeSpan = KeyLifeSpan,
                Key = key,
                ServiceName = request.ServiceName,
                SessionKey = request.SessionKey
            };

            var serviceResponse = new ResponseForService
            {
                ClientIdentity = request.ClientIdentity,
                SessionLifeSpan = KeyLifeSpan,
                Key = key
            };

            var pair = new KeyManagerAuthResponse
            {
                ClientMessage = JsonConvert.SerializeObject(clientResponse),
                ServiceMessage = JsonConvert.SerializeObject(serviceResponse),
            };

            response = JsonConvert.SerializeObject(pair);

            return true;
        }

    }
}