using System;
using System.Collections.Generic;
using Authorizer.Interfaces;
using Authorizer.Models;
using Newtonsoft.Json;

namespace Authorizer.Implementations
{
    public class BasicClient : IClient
    {
        private IKeyManager KeyManager { get; set; }
        private string ManagerKey { get; set; }
        private IDictionary<IPrivateService, string> Keys { get; set; }
        public string Identity { get; private set; }

        public BasicClient(string identity, IKeyManager keyManager)
        {
            Identity = identity;
            KeyManager = keyManager;
            ManagerKey = keyManager.GetInitialKey(Identity);
            Keys = new Dictionary<IPrivateService, string>();
        }

        public bool GetKeyForService(IPrivateService service)
        {
            var rand = new Random().Next(100000, 999999);

            var request = new ClientRequest()
            {
                ClientIdentity = Identity,
                ServiceName = service.Name,
                SessionKey = rand.ToString()
            };

            var message = JsonConvert.SerializeObject(request);
            string responseMessage;

            if (KeyManager.GetKeyForService(message, out responseMessage))
            {
                var response = JsonConvert.DeserializeObject<KeyManagerAuthResponse>(responseMessage);

                if (response == null) return false;

                var clientMessage = JsonConvert.DeserializeObject<ResponseForClient>(response.ClientMessage);

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