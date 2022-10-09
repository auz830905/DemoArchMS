using Microsoft.EntityFrameworkCore;
using MSProfesores.Extensions;
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

//builder.Services.AddDbContext<DBContextProfesores>(options =>
//{
//    var connections = Configuration.GetConnectionString("DefaultConnectionMySQL");
//    options.UseMySql(connections, ServerVersion.AutoDetect(connections));
//});

builder.Services.AddEntityFrameworkSqlServer().AddDbContext<DBContextProfesores>(options =>
{
    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionSQLServer"));    
});

var app = builder.Build();

app.UseCors(cors => cors
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials()
);

app.ConfigureSwagger();
app.UseHttpsRedirection();
app.MapControllers();

app.AddEndPointsProfesoresExtension();

app.Run();