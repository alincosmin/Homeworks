using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Authorizer.Interfaces;
using Authorizer.Models;
using Newtonsoft.Json;

namespace Authorizer.Implementations
{
    public class BasicKeyManager : IKeyManager
    {
        public TimeSpan KeyLifeSpan { get; set; }

        private readonly IDictionary<IClient, byte[]> _clientKeys = new Dictionary<IClient, byte[]>();
        private readonly IDictionary<IPrivateService, byte[]> _serviceKeys = new Dictionary<IPrivateService, byte[]>();

        public byte[] Greet(object obj)
        {
            var rnd = new Random();
            var key = new byte[24];
            rnd.NextBytes(key);

            if (obj is IClient)
            {
                _clientKeys.Add((IClient) obj, key);
                return key;
            }
            else if (obj is IPrivateService)
            {
                _serviceKeys.Add((IPrivateService) obj, key);
                return key;
            }

            return null;
        }

        public bool GetKeyForService(IClient basicClient, string message, out string response)
        {
            response = "";
            var request = JsonConvert.DeserializeObject<ClientRequest>(message);

            var clientDes = new TripleDESCryptoServiceProvider();
            clientDes.Key = _clientKeys[basicClient];
            var clientCrypto = new TripleDESWrapper(clientDes);

            var serviceDes = new TripleDESCryptoServiceProvider();
            serviceDes.Key = _serviceKeys.FirstOrDefault(x => x.Key.Name.Equals(request.ServiceName)).Value;
            var serviceCrypto = new TripleDESWrapper(clientDes);

            var rnd = new Random();
            var key = new byte[16];
            rnd.NextBytes(key);

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
                ClientMessage = clientCrypto.Encrypt(JsonConvert.SerializeObject(clientResponse)),
                ServiceMessage = serviceCrypto.Encrypt(JsonConvert.SerializeObject(serviceResponse)),
            };

            response = JsonConvert.SerializeObject(pair);

            return true;
        }


    }
}