using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace MicroserviceDuendeTemplate.Base.Definition
{
    public interface IDefinition 
    {
        bool Enabled { get; }

        void ConfigureServicesAsync(IServiceCollection services, WebApplicationBuilder builder);

        void ConfigureApplicationAsync(WebApplication app);
    }
}
