namespace Authorizer.Interfaces
{
    public interface IClient
    {
        string Identity { get; }
        bool GetKeyForService(IPrivateService service);
        bool SendMessageToService(IPrivateService service, string message);
    }
}