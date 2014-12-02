using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Script.Serialization;
using Authorizer.Interfaces;
using Authorizer.Models;

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
            var json = new JavaScriptSerializer();

            var rand = new Random().Next(100000, 999999);

            var request = new ClientRequest()
            {
                ClientIdentity = Identity,
                ServiceName = service.Name,
                SessionKey = rand.ToString()
            };

            var message = json.Serialize(request);
            string responseMessage;

            if (KeyManager.GetKeyForService(message, out responseMessage))
            {
                var response = new JavaScriptSerializer().Deserialize<KeyManagerAuthResponse>(responseMessage);

                if (response == null) return false;
                
                var clientMessage = json.Deserialize<ResponseForClient>(response.ClientMessage);
                

                Keys.Add(service, clientMessage.Key);
                service.InitialConnection(response.ServiceMessage);
                return true;    
            }

            return false;    
        }

        public bool SendMessageToService(IPrivateService service, string message)
        {
            Console.WriteLine("Sending message \"{0}\" to service {1}", message, service.Name);            
            return true;
        }
    }
}