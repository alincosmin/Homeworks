namespace Authorizer
{
    public interface IClient
    {
        string Identity { get; }
        bool GetKeyForService(string serviceName);
        bool SendMessageToService(string serviceName);
    }
}