using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Authorizer
{
    public class BasicClient : IClient
    {
        private IKeyManager KeyManager { get; set; }
        private string ManagerKey { get; set; }
        private IDictionary<string, string> Keys { get; set; }
        public string Identity { get; private set; }

        public BasicClient(string identity, IKeyManager keyManager)
        {
            Identity = identity;
            KeyManager = keyManager;
            ManagerKey = keyManager.GetInitialKey(Identity);
            Keys = new Dictionary<string, string>();
        }

        public bool GetKeyForService(string serviceName, IPrivateService service)
        {
            var json = new JavaScriptSerializer();

            var rand = new Random().Next(100000, 999999);

            var request = new ClientRequest()
            {
                ClientIdentity = Identity,
                ServiceName = serviceName,
                SessionKey = rand.ToString()
            };

            var message = json.Serialize(request);
            string responseMessage;
            KeyManagerAuthResponse response;

            if (KeyManager.GetKeyForService(message, out responseMessage))
            {
                response = new JavaScriptSerializer().Deserialize<KeyManagerAuthResponse>(responseMessage);
                var clientMessage = json.Deserialize<ResponseForClient>(response.ClientMessage);
                Keys.Add(clientMessage.ServiceName, clientMessage.Key);
                service.InitialConnection(response.ServiceMessage);
                return true;    
            }

            return false;    
        }

        public bool SendMessageToService(string serviceName)
        {
            if (!Keys.ContainsKey(serviceName)) return false;

            return true;
        }
    }
}