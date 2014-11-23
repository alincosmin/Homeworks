using System.Collections.Generic;

namespace Authorizer
{
    public class BasicClient : IClient
    {
        private IKeyManager KeyManager { get; set; }
        private IDictionary<string, string> Keys { get; set; } 

        public BasicClient(IKeyManager keyManager)
        {
            KeyManager = keyManager;
            Keys = new Dictionary<string, string>();
        }

        public bool GetKeyForService(string serviceName)
        {
            string key;
            if (!KeyManager.GetKeyForService(serviceName, out key)) return false;
            Keys[serviceName] = key;
            return true;
        }

        public bool SendMessageToService(string serviceName)
        {
            if (!Keys.ContainsKey(serviceName)) return false;

            return true;
        }
    }
}