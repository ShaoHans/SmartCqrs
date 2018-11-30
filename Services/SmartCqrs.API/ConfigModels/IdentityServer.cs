namespace SmartCqrs.API.ConfigModels
{
    public class IdentityServer
    {
        public string CommonServiceHost { get; set; }
        public string AuthTokenUrl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
