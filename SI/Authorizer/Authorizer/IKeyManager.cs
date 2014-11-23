namespace Authorizer
{
    public interface IKeyManager
    {
        string GetInitialKey(string identity);
        bool GetKeyForService(string identity, out string response);
    }
}
