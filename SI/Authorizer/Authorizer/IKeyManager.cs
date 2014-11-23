namespace Authorizer
{
    public interface IKeyManager
    {
        bool GetKeyForService(string serviceName, out string key);
    }
}
