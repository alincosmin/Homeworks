using System;

namespace Authorizer.Models
{
    public class ResponseForService
    {
        public string Key { get; set; }
        public string ClientIdentity { get; set; }
        public TimeSpan SessionLifeSpan { get; set; }
    }
}