using System;

namespace Authorizer.Interfaces
{
    public interface IKeyManager
    {
        string GetInitialKey(string identity);
        bool GetKeyForService(string message, out string response);
        TimeSpan KeyLifeSpan { get; set; }
    }
}
