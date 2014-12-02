﻿using System;

namespace Authorizer.Models
{
    public class ResponseForClient
    {
        public string Key { get; set; }
        public string SessionKey { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string ServiceName { get; set; }
    }
}