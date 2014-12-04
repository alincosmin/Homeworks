using PrivateServiceClient.SimpleService;
using PrivateServiceClient.ServiceManager;

namespace PrivateServiceClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new SimpleServiceClient();
            var client = new Client(new ServiceManagerClient(), service);
            
            client.SendMessage("mesaj");
        }


    }
}
