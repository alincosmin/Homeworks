using System;

namespace Authorizer.Models.Models
{
    public class KeyValidTime
    {
        public Guid Id { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}