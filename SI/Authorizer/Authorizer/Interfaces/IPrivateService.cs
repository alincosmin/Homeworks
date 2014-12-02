namespace Authorizer.Interfaces
{
    public interface IPrivateService
    {
        bool InitialConnection(string message);
        string Name { get; }
        void ProcessMessage(string message);
    }
}