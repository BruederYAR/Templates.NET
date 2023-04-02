using Brueder.Architecture.Base.Definition;
using Microsoft.OpenApi.Models;

namespace MicroserviceDuendeTemplate.Identity.Definitions.Swagger
{
    public class SwaggerDefinition : Definition
    {
        private const string _swaggerConfig = "/swagger/v1/swagger.json";

        public override void ConfigureServicesAsync(IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Identity Service Api", Version = "v1" });
                c.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n  Enter 'Bearer' [space] and then your token in the text input below.",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    }
                );

                c.AddSecurityRequirement(
                    new OpenApiSecurityRequirement()
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header,
                            },
                            new List<string>()
                        }
                    }
                );
            });
        }

        public override void ConfigureApplicationAsync(WebApplication app)
        {
            // if (!app.Environment.IsDevelopment())
            // {
            //     return;
            // }
            
            app.UseSwagger();
            app.UseSwaggerUI(settings =>
            {
                // settings.SwaggerEndpoint(_swaggerConfig, $"api");
                // settings.DefaultModelExpandDepth(0);
                // settings.DefaultModelsExpandDepth(0);
                // settings.OAuthScopeSeparator(" ");
                // settings.OAuthClientId("client-id-code");
                // settings.OAuthClientSecret("client-secret-code");
                // settings.DisplayRequestDuration();
            });
        }
    }
}
