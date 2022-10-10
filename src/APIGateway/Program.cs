using APIGateway.Agregators;
using APIGateway.Handles;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddCors();

//builder.Services.AddOcelot()
//    .AddDelegatingHandler<NoEncodingHandler>(true);

builder.Services.AddOcelot()
    .AddSingletonDefinedAggregator<ClasesImpartidasPorUnProfesorAggregator>();

var app = builder.Build();

app.UseCors(cors => cors
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials()    
);

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseOcelot().Wait();

app.MapControllers();

app.Run();