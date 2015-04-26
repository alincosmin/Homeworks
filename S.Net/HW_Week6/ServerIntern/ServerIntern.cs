using System;
using System.ServiceModel;
using WCFIntern;

namespace ServerIntern
{
    class ServerIntern
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(Intern),
            new Uri("http://localhost:8182/intern"));
            host.Open();
            Console.WriteLine("Server intern started...");
            Console.ReadLine();
            host.Close();
        }
    }
}
