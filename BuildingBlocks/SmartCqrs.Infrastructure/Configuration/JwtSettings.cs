using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCqrs.Infrastructure.Configuration
{
    public class JwtSettings
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string SecurityKey { get; set; }
    }
}
