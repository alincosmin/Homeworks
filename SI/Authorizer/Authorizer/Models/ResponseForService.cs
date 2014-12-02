using System;

namespace Authorizer.Models
{
    public class ResponseForService
    {
        public byte[] Key { get; set; }
        public string ClientIdentity { get; set; }
        public TimeSpan SessionLifeSpan { get; set; }
    }
}