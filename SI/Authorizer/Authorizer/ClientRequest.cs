using System.Security.Policy;

namespace Authorizer
{
    public class ClientRequest
    {
        public string ClientIdentity { get; set; }
        public string ServiceName { get; set; }
        public string SessionKey { get; set; }
    }
}