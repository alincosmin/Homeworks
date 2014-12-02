using System;
using Authorizer.Interfaces;

namespace Authorizer.Implementations
{
    public class BasicKeyManager : IKeyManager
    {
        public string GetInitialKey(string identity)
        {
            Console.WriteLine("Received request for initial connection");
            return "";
        }

        public bool GetKeyForService(string message, out string response)
        {
            Console.WriteLine("Received request with message: {0}", message);
            response = "";
            return true;
        }
    }
}