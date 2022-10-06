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

/*builder.Services.AddDbContext<DBContextProfesores>(options =>
{
    options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
              
});*/

builder.Services.AddEntityFrameworkSqlServer().AddDbContext<DBContextProfesores>(options =>
{
    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionSQLServer"));
});


var app = builder.Build();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyMethod());
app.ConfigureSwagger();
app.UseHttpsRedirection();
app.MapControllers();

app.AddEndPointsProfesoresExtension();

app.Run();