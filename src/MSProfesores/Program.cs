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

app.MapGet("/GetProfesorByCI/{ci}", async (IProfesoresRepository repository, string ci) =>
{
    var profesor = await repository.GetProfesor(ci);

    if (profesor != null)
        return Results.Ok(profesor);

    return Results.NotFound();
})
.WithName("GetProfesorByCI");

app.MapGet("/GetProfesores", async (IProfesoresRepository repository) =>
{
    var profesores = await repository.GetProfesores();

    if (profesores != null)
        return Results.Ok(profesores);

    return Results.NoContent();
})
.WithName("GetProfesores");

app.MapPost("/PostProfesor", async (IProfesoresRepository repository,  Profesor profesor) =>
{
    var result = await repository.AddProfesor(profesor);
    if (result)
        return Results.NoContent();
    return Results.Problem(statusCode: 500, title: "Internal error");
})
.WithName("PostProfesor");

app.Run();