using Authorizer.Implementations;

namespace Authorizer.Interfaces
{
    public interface IPrivateService
    {
        bool InitialConnection(string message, IClient client);
        string Name { get; }
        bool ProcessMessage(string message, IClient client);
    }
}