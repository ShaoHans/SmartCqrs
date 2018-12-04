using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCqrs.Infrastructure.Configuration
{
    public class IdentityServer
    {
        public string CommonServiceHost { get; set; }
        public string AuthTokenUrl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string JwtSecurityKey { get; set; }
    }
}
