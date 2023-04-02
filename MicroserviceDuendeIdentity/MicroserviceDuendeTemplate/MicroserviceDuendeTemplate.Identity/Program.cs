using Brueder.Architecture.Base.Definition;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDefinitions(builder, typeof(Program));

builder.Services.AddMvcCore()
                .AddApiExplorer();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseDefinitions();

app.UseHttpsRedirection();

app.Run();