﻿using MicroserviceDuendeTemplate.DAL.Database;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Brueder.Architecture.Base.Definition;

namespace MicroserviceDuendeTemplate.Identity.Definitions.Database
{
    public class DatabaseDefinition : Definition
    {
        public override void ConfigureServicesAsync(IServiceCollection services, WebApplicationBuilder builder)
        {
            string migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? "Server=localhost;Port=5432;User Id=postgres;Password=password;Database=Microservice.Identity";

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString,
                b => b.MigrationsAssembly(migrationsAssembly)));
        }
    }
}
