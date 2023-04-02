using Brueder.Architecture.Base.Definition;
using MicroserviceDuendeTemplate.DAL.Models.Options;
using MicroserviceDuendeTemplate.Identity.Definitions.Identity.Model;

namespace MicroserviceDuendeTemplate.Identity.Definitions.Options;

public class OptionsDefinition : Definition
{
    public override void ConfigureServicesAsync(IServiceCollection services, WebApplicationBuilder builder)
    {
        var configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
        configurationBuilder.AddJsonFile("identitysetting.json");
        IConfiguration identityConfiguration = configurationBuilder.Build();

        var url = new IdentityAddressOption() { Url = identityConfiguration.GetValue<string>("Url") };
        services.AddSingleton(url);
        
        services.Configure<ClientIdentity>(identityConfiguration.GetSection("CurrentIdentityClient"));
        services.Configure<AdminUser>(builder.Configuration.GetSection("AdminUser"));
    }
}
