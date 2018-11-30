using System;

namespace SmartCqrs.Infrastructure.CommonServices
{
    public class CaptchaResponseDto
    {
        public string SendSmsResponse { get; set; }

        public DateTime Validtabletime { get; set; }

        public int Validtimelen { get; set; }
    }
}
