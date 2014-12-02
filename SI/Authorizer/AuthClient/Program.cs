﻿using System;
using System.Threading;
using Authorizer.Implementations;

namespace AuthClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-- Program start: {0}", DateTime.Now);
            var keyManager = new BasicKeyManager
            {
                KeyLifeSpan = new TimeSpan(2, 0, 0)
            };

            var client = new BasicClient("ClientSimplu", keyManager);
            var service = new BasicService {Name = "ServSimplu"};
            Thread.Sleep(2000);
            client.GetKeyForService(service);
            client.SendMessageToService(service, "mare mesaj");
        }
    }
}
