using Brueder.Architecture.Base.Definition;
using Duende.IdentityServer.AspNetIdentity;
using Duende.IdentityServer.Services;
using MicroserviceDuendeTemplate.DAL.Database;
using MicroserviceDuendeTemplate.DAL.Models.Identity;
using MicroserviceDuendeTemplate.Identity.Definitions.Identity.Model;
using Microsoft.AspNetCore.Identity;

namespace MicroserviceDuendeTemplate.Identity.Definitions.Identity
{
    public class IdentityDefinition : Definition
    {
        public override void ConfigureServicesAsync(IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddAuthorization();
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZабвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ ";
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddRoles<ApplicationRole>()
            .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            });
            
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
            configurationBuilder.AddJsonFile("identitysetting.json");
            IConfiguration identityConfiguration = configurationBuilder.Build();

            var clients = identityConfiguration.GetSection("ClientsIdentity").Get<List<ClientIdentity>>();
            var scopes = identityConfiguration.GetSection("Scopes").Get<List<IdentityScopeOption>>();
            
            services.AddIdentityServer()
                .AddInMemoryIdentityResources(IdentitySettings.GetIdentityResources())
                .AddInMemoryClients(IdentitySettings.GetClients(clients))
                .AddInMemoryApiResources(IdentitySettings.GetApiResources(scopes))
                .AddInMemoryApiScopes(IdentitySettings.GetApiScopes(scopes))
                .AddAspNetIdentity<ApplicationUser>()
                .AddDeveloperSigningCredential();

            services.AddScoped<IProfileService, ProfileService<ApplicationUser>>();
        }
        
        public override void ConfigureApplicationAsync(WebApplication app)
        {
            app.UseIdentityServer();
            app.UseAuthorization();
        }
    }
}
