using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Authorizer.Models;
using Authorizer.Models.Models;
using Newtonsoft.Json;

namespace PrivateService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceManager : IServiceManager
    {
        private IDictionary<Guid, string> _keys;
        private Random _randomGenerator;
        private readonly TimeSpan _keyLifeSpan;

        public ServiceManager()
        {
            _keys = new Dictionary<Guid, string>(); 
            _randomGenerator = new Random();
            _keyLifeSpan = new TimeSpan(2, 0, 0);
        }

        public string InitialConnect(Guid id)
        {
            var key = new byte[24];
            _randomGenerator.NextBytes(key);

            _keys.Add(id, Convert.ToBase64String(key));
            return Convert.ToBase64String(key);
        }

        public string[] RequestToAccessService(string message)
        {
            var request = JsonConvert.DeserializeObject<RequestAccessModel>(message);
            var key = new byte[16];
            _randomGenerator.NextBytes(key);

            var expireDate = DateTime.Now.Add(_keyLifeSpan);

            var clientResponse = new ResponseForClientModel
            {
                Key = Convert.ToBase64String(key),
                ServiceId = request.ServiceId,
                SessionExpirationDate = expireDate,
                SessionKey = request.SessionKey
            };

            var serviceResponse = new ResponseForServiceModel
            {
                Key = Convert.ToBase64String(key),
                ClientId = request.ClientId,
                SessionExpirationDate = expireDate
            };

            var clientResponseSerialized = JsonConvert.SerializeObject(clientResponse);
            var serviceResponseSerialized = JsonConvert.SerializeObject(serviceResponse);
            var clientKey = _keys[request.ClientId];
            var serviceKey = _keys[request.ServiceId];

            return new[]
            {
                TripleDESWrapper.Encrypt(clientResponseSerialized, clientKey),
                TripleDESWrapper.Encrypt(serviceResponseSerialized, serviceKey)
            };
        }
    }
}
