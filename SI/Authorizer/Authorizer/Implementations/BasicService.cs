using System;
using Authorizer.Interfaces;

namespace Authorizer.Implementations
{
    public class BasicService : IPrivateService
    {
        public bool InitialConnection(string message)
        {
            Console.WriteLine("Initial connection with message: {0}", message);
            return true;
        }

        public string Name { get; set; }
    }
}