using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SmartCqrs.API
{
    public static class ServiceCollectionExtensions
    {
        public static void CommonserviceUrl(this IServiceCollection services,
            IConfiguration configuration)
        {
            var commonserviceUrlModel = new CommonserviceUrlModel();
            configuration.GetSection("CommonserviceUrl").Bind(commonserviceUrlModel);
        }
    }

    public class CommonserviceUrlModel
    {
        public string OffLinePush { get; set; }
        public string OnLinePush { get; set; }
    }
}
