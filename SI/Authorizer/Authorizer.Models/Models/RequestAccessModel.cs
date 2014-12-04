using System;

namespace Authorizer.Models.Models
{
    public class RequestAccessModel
    {
        public Guid ClientId { get; set; }
        public Guid ServiceId { get; set; }
        public string SessionKey { get; set; }
    }
}