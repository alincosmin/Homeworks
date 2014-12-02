using Authorizer.Implementations;

namespace AuthClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var keyManager = new BasicKeyManager();
            var client = new BasicClient("CkientSimplu", keyManager);
            var service = new BasicService {Name = "ServSimplu"};
            client.GetKeyForService(service);
            client.SendMessageToService(service, "mare mesaj");
        }
    }
}
