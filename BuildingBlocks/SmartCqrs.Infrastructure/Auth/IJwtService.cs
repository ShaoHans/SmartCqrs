using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCqrs.Infrastructure.Auth
{
    public interface IJwtService
    {
        string GenerateToken(ClientUser user);

        bool ValidateToken(string token);
    }
}
