using System;

namespace Authorizer.Models
{
    public class ResponseForClient
    {
        public byte[] Key { get; set; }
        public string SessionKey { get; set; }
        public TimeSpan SessionLifeSpan { get; set; }
        public string ServiceName { get; set; }
    }
}