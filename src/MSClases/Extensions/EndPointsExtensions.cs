using System;
using MSClases.Interfaces;
using MSClases.Models;

namespace MSClases.Extensions
{
	internal static class EndPointsExtensions
	{
        internal static void AddEndPointsClasesExtension(this WebApplication app)
        {
            app.MapGet("/GetClasesById/{Id}", async (IClasesRepository repository, int Id) =>
            {
                var clase = await repository.GetClase(Id);

                if (clase != null)
                    return Results.Ok(clase);

                return Results.NotFound();
            })
            .WithName("GetClasesById");

            app.MapGet("/GetClases", async (IClasesRepository repository) =>
            {
                var clases = await repository.GetClases();

                if (clases != null)
                    return Results.Ok(clases);

                return Results.NoContent();
            })
            .WithName("GetProfesores");

            app.MapPost("/PostClase", async (IClasesRepository repository, Clase clase) =>
            {
                var result = await repository.AddClase(clase);
                if (result)
                    return Results.NoContent();
                return Results.Problem(statusCode: 500, title: "Internal error");
            })
            .WithName("PostClase");
        }

        internal static void AddEndPointClasesProfesoresExtensions(this WebApplication app)
        {
            app.MapGet("/GetClasesByProfesorId/{Ci}", async (IClasesProfesoresRepository repository, string Ci) =>
            {
                var clases = await repository.GetClasesByProfesor(Ci);

                if (clases != null)
                    return Results.Ok(clases);

                return Results.NoContent();
            })
            .WithName("GetClasesByProfesorId");

            app.MapPost("/PostClaseProfesor/{IdClase}/{Ci}", async (IClasesProfesoresRepository repository, int IdClase, string Ci) =>
            {
                var result = await repository.AddProfesorAClase(Ci, IdClase);
                if (result)
                    return Results.NoContent();
                return Results.Problem(statusCode: 500, title: "Internal error");
            })
            .WithName("PostClaseProfesor");
        }

    }
}

