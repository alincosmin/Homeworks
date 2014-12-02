using System;
using Authorizer.Interfaces;
using Authorizer.Models;
using Newtonsoft.Json;

namespace Authorizer.Implementations
{
    public class BasicService : IPrivateService
    {
        public string Name { get; set; }

        public bool InitialConnection(string message)
        {
            var request = JsonConvert.DeserializeObject<ResponseForService>(message);

            if (request.ExpirationDate.CompareTo(DateTime.Now) > 0)
            {
                Console.WriteLine("Client {0} wants something. His access will last until {1}", request.ClientIdentity,
                request.ExpirationDate);

                return true;
            }

            Console.WriteLine("Client {0} wants something, but session expired ({1})", request.ClientIdentity, request.ExpirationDate);
            return false;
        }

        public void ProcessMessage(string message)
        {
            
        }
    }
}