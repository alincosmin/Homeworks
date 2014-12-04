using System;

namespace Authorizer.Models.Models
{
    public class ResponseForClientModel
    {
        public string Key { get; set; }
        public string SessionKey { get; set; }
        public DateTime SessionExpirationDate { get; set; }
        public Guid ServiceId { get; set; }
    }
}