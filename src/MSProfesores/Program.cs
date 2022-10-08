using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MSProfesores.Extensions;
using MSProfesores.Interfaces;
using MSProfesores.Models;

var builder = WebApplication.CreateBuilder(args);
IConfiguration Configuration = builder.Configuration;

builder.Host.ConfigureLogging(logging => {
    logging.ClearProviders();
    logging.AddConsole();
});

builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddSwaggerExtension();
builder.Services.AddInfraestructure();

builder.Services.AddDbContext<DBContextProfesores>(options =>
{
    var connections = Configuration.GetConnectionString("DefaultConnectionMySQL");
    options.UseMySql(connections, ServerVersion.AutoDetect(connections));
});

var app = builder.Build();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyMethod());
app.ConfigureSwagger();
app.UseHttpsRedirection();
app.MapControllers();

app.AddEndPointsProfesoresExtension();

app.Run();