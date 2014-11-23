namespace Authorizer
{
    public interface IClient
    {
        bool GetKeyForService(string serviceName);
        bool SendMessageToService(string serviceName);
    }
}