using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MSClases.Extensions;
using MSClases.Interfaces;
using MSClases.Models;

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

builder.Services.AddDbContext<DBContextClases>(options =>
{
    options.UseNpgsql(Configuration.GetConnectionString("PostgreSQL"));
              
});

var app = builder.Build();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyMethod());
app.ConfigureSwagger();
app.UseHttpsRedirection();
app.MapControllers();

app.AddEndPointsClasesExtension();
app.AddEndPointClasesProfesoresExtensions();

app.Run();