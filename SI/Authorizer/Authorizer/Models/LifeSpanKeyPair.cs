using System;

namespace Authorizer.Models
{
    public class LifeSpanKeyPair
    {
        public DateTime ExpirationDate { get; set; }
        public byte[] Key { get; set; }
    }
}