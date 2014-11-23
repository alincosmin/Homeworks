using System;

namespace Authorizer
{
    public class ResponseForService
    {
        public string Key { get; set; }
        public string ClientIdentity { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}