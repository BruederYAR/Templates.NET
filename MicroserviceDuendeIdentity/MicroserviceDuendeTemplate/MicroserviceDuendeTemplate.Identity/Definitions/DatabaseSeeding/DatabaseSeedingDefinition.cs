using Brueder.Architecture.Base.Definition;
using MicroserviceDuendeTemplate.DAL.Database;

namespace MicroserviceDuendeTemplate.Identity.Definitions.DatabaseSeeding;

public class DatabaseSeedingDefinition : Definition
{
    public override async void ConfigureApplicationAsync(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetService<ApplicationDbContext>()!;

        await new DatabaseInitializer(app.Services, context).Seed();
    }
}
