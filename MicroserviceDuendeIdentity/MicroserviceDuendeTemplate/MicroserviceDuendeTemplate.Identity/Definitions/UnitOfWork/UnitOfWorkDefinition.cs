using Brueder.Architecture.Base.Definition;
using Brueder.Architecture.Base.UnitOfWork;
using MicroserviceDuendeTemplate.DAL.Database;

namespace MicroserviceDuendeTemplate.Identity.Definitions.UnitOfWork
{
    public class UnitOfWorkDefinition : Definition
    {
        public override void ConfigureServicesAsync(IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddUnitOfWork<ApplicationDbContext>();
        }
    }
}
