using System;

namespace Authorizer.Models.Models
{
    public class ResponseForServiceModel
    {
        public string Key { get; set; }
        public Guid ClientId { get; set; }
        public DateTime SessionExpirationDate { get; set; }
    }
}