using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Authorizer.Interfaces;
using Authorizer.Models;
using Newtonsoft.Json;

namespace Authorizer.Implementations
{
    public class BasicClient : IClient
    {
        private IKeyManager KeyManager { get; set; }
        private readonly byte[] _managerKey;
        private IDictionary<IPrivateService, byte[]> Keys { get; set; }
        public string Identity { get; private set; }

        public BasicClient(string identity, IKeyManager keyManager)
        {
            Identity = identity;
            KeyManager = keyManager;
            Keys = new Dictionary<IPrivateService, byte[]>();
            _managerKey = keyManager.Greet(this);
        }

        public bool GetKeyForService(IPrivateService service)
        {
            var rand = (new Random().Next(100000, 999999)).ToString();

            var request = new ClientRequest()
            {
                ClientIdentity = Identity,
                ServiceName = service.Name,
                SessionKey = rand
            };

            var message = JsonConvert.SerializeObject(request);

            string responseMessage;

            if (KeyManager.GetKeyForService(this, message, out responseMessage))
            {
                var response = JsonConvert.DeserializeObject<KeyManagerAuthResponse>(responseMessage);

                if (response == null) return false;

                var decryptMessage = TripleDESWrapper.Decrypt(response.ClientMessage, _managerKey);
                var clientMessage = JsonConvert.DeserializeObject<ResponseForClient>(decryptMessage);

                if(!clientMessage.SessionKey.Equals(rand)) return false;

                Keys.Add(service, clientMessage.Key);

                service.InitialConnection(response.ServiceMessage);
                return true;    
            }

            return false;    
        }

        public bool SendMessageToService(IPrivateService service, string message)
        {
            service.ProcessMessage(message);
            return true;
        }
    }
}