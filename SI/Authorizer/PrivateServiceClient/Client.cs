using System;
using System.Collections.Generic;
using System.Linq;
using Authorizer.Models;
using Authorizer.Models.Models;
using Newtonsoft.Json;
using PrivateServiceClient.ServiceManager;
using PrivateServiceClient.SimpleService;

namespace PrivateServiceClient
{
    public class Client
    {
        public Guid Id { get; private set; }
        private string _managerKey { get; set; }
        public IServiceManager ServiceManager { get; set; }
        public ISimpleService Service { get; set; }
        private string _serviceKey { get; set; }
        private DateTime _keyValid { get; set; }

        public Client(IServiceManager manager, ISimpleService service)
        {
            Id = Guid.NewGuid();
            Service = service;
            ServiceManager = manager;
            _managerKey = ServiceManager.InitialConnect(Id);
        }

        public void SendMessage(string message)
        {
            if (string.IsNullOrEmpty(_serviceKey))
            {
                RequestAccess();
            }

            if (string.IsNullOrEmpty(_serviceKey)) return;
            var encMessage = TripleDESWrapper.Encrypt(message, _serviceKey);

            Console.WriteLine("Client: Sent [{0}] as [{1}]", message, encMessage);
            var serviceResponse = Service.ProcessMessage(encMessage, Id);
            Console.WriteLine("Service: {0}", serviceResponse);

        }

        private void RequestAccess()
        {
            var sessionKey = new byte[8];
            new Random().NextBytes(sessionKey);

            var request = new RequestAccessModel
            {
                ClientId = Id,
                ServiceId = Service.get_Id(),
                SessionKey = Convert.ToBase64String(sessionKey)
            };

            var requestSerialized = JsonConvert.SerializeObject(request);

            var managerResponse = ServiceManager.RequestToAccessService(requestSerialized);
            if (managerResponse != null)
            {
                var decResponse = TripleDESWrapper.Decrypt(managerResponse[0], _managerKey);
                var response = JsonConvert.DeserializeObject<ResponseForClientModel>(decResponse);

                var isSessionOk = response.SessionKey.Equals(Convert.ToBase64String(sessionKey));
                var isServiceOk = response.ServiceId.Equals(Service.get_Id());
                var isTimeOk = response.SessionExpirationDate.CompareTo(DateTime.Now) > 0;

                if (isSessionOk && isServiceOk && isTimeOk)
                {
                    _serviceKey = response.Key;
                    _keyValid = response.SessionExpirationDate;

                    //Console.WriteLine("Client: Sent manager response to service.");

                    var pair = JsonConvert.SerializeObject(new KeyValidTime
                    {
                        Id = this.Id,
                        ExpirationDate = _keyValid
                    });

                    var newMessage = new[]
                    {
                        managerResponse[1],
                        TripleDESWrapper.Encrypt(pair, _serviceKey)
                    };

                    var serviceResponse = Service.IsAllowed(newMessage);
                    
                    if(!serviceResponse) Console.WriteLine("Client: Not allowed!");

                }
                else
                {
                    Console.WriteLine("Client:  Session: {0}, Service: {1}, Time: {2}", isSessionOk, isServiceOk, isTimeOk);
                }

                
            }
            else
            {
                Console.WriteLine("Client: Unauthorized or can't communicate with manager!");
            }
        }
    }
}