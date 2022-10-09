using Microsoft.AspNetCore.Mvc;
using MSProfesores.Interfaces;
using MSProfesores.Models;

namespace MSProfesores.Extensions
{
	internal static class EndPointExtensions
	{
		internal static void AddEndPointsProfesoresExtension(this WebApplication app)
		{
            app.MapGet("/api/profesores/{Ci}", async (IProfesoresRepository repository, string Ci) =>
            {
                var profesor = await repository.GetProfesor(Ci);

                if (profesor != null)
                    return Results.Ok(profesor);

                return Results.NotFound();
            })
            .WithName("GetProfesorByCI");

            app.MapGet("/api/profesores", async (IProfesoresRepository repository) =>
            {
                var profesores = await repository.GetProfesores();

                if (profesores != null)
                    return Results.Ok(profesores);

                return Results.NoContent();
            })
            .WithName("GetProfesores");

            app.MapPost("/api/profesores", async (IProfesoresRepository repository, [FromBody] Profesor profesor) =>
            {
                var result = await repository.AddProfesor(profesor);
                if (result)
                    return Results.NoContent();
                return Results.Problem(statusCode: 500, title: "Internal error");
            })
            .WithName("PostProfesor");

            app.MapDelete("/api/profesores/{Ci}", async (IProfesoresRepository repository, string Ci) =>
            {
                var result = await repository.DeleteProfesor(Ci);
                if (result)
                    return Results.NoContent();
                return Results.Problem(statusCode: 500, title: "Internal error");
            })
            .WithName("DeleteProfesor");
        }

    }
}

