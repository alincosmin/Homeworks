using System;
using Authorizer.Implementations;

namespace Authorizer.Interfaces
{
    public interface IKeyManager
    {
        bool GetKeyForService(IClient basicClient, string message, out string response);
        TimeSpan KeyLifeSpan { get; set; }
        byte[] Greet(object obj);
    }
}
