using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Authorizer.Models;
using Authorizer.Models.Models;
using Newtonsoft.Json;
using PrivateService.ServiceReference1;

namespace PrivateService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class SimpleService : ISimpleService
    {
        public Guid Id { get; private set; }
        private readonly ServiceManagerClient ServiceManager;
        private string _managerKey { get; set; }
        private IDictionary<KeyValidTime, string> _keys { get; set; } 

        public SimpleService()
        {
            Id = Guid.NewGuid();
            ServiceManager = new ServiceManagerClient();
            _managerKey = ServiceManager.InitialConnect(Id);
            _keys = new Dictionary<KeyValidTime, string>();
        }

        public string ProcessMessage(string message, Guid clientId)
        {
            var pair = _keys.Keys.FirstOrDefault(x => x.Id.Equals(clientId));
            if (pair != null)
            {
                var decodedMessage = TripleDESWrapper.Decrypt(message, _keys[pair]);
                return string.Format("Received [{0}] as [{1}]", decodedMessage, message);
            }

            return "401";
        }

        public bool IsAllowed(string[] message)
        {
            var decManagerResponse = TripleDESWrapper.Decrypt(message[0], _managerKey);
            var managerResponse = JsonConvert.DeserializeObject<ResponseForServiceModel>(decManagerResponse);
            var key = managerResponse.Key;
            var isTimeOk = managerResponse.SessionExpirationDate.CompareTo(DateTime.Now) > 0;
            if (isTimeOk)
            {
                var decClientResponse = TripleDESWrapper.Decrypt(message[1], key);
                var clientReq = JsonConvert.DeserializeObject<KeyValidTime>(decClientResponse);

                var isClientOk = managerResponse.ClientId.Equals(clientReq.Id);
                var isTimeOk2 = managerResponse.SessionExpirationDate.CompareTo(clientReq.ExpirationDate) == 0;

                if (isClientOk && isTimeOk2)
                {
                    _keys.Add(clientReq, key);
                    return true;
                }
            }

            return false;
        }
    }
}
