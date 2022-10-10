using MSClases.Interfaces;
using MSClases.Models;

namespace MSClases.Extensions
{
	internal static class EndPointsExtensions
	{
        internal static void AddEndPointsClasesExtension(this WebApplication app)
        {
            app.MapGet("/api/clases/{Id}", async (IClasesRepository repository, int Id) =>
            {
                var clase = await repository.GetClase(Id);

                if (clase != null)
                    return Results.Ok(clase);

                return Results.NotFound();
            })
            .WithName("GetClasesById");

            app.MapGet("/api/clases", async (IClasesRepository repository) =>
            {
                var clases = await repository.GetClases();

                if (clases != null)
                    return Results.Ok(clases);

                return Results.NoContent();
            })
            .WithName("GetClases");

            app.MapPost("/api/clases", async (IClasesRepository repository, Clase clase) =>
            {
                var result = await repository.AddClase(clase);
                if (result)
                    return Results.NoContent();
                return Results.Problem(statusCode: 500, title: "Internal error");
            })
            .WithName("PostClase");

            app.MapDelete("/api/clases/{Id}", async (IClasesRepository repository, int Id) =>
            {
                var result = await repository.DeleteClase(Id);
                if (result)
                    return Results.NoContent();
                return Results.Problem(statusCode: 500, title: "Internal error");
            })
            .WithName("DeleteClase");
        }

        internal static void AddEndPointClasesProfesoresExtensions(this WebApplication app)
        {
            app.MapGet("/api/clasesprofesores/{Ci}", async (IClasesProfesoresRepository repository, string Ci) =>
            {
                var clases = await repository.GetClasesByProfesor(Ci);

                if (clases != null)
                    return Results.Ok(clases);

                return Results.NoContent();
            })
            .WithName("GetClasesByProfesorId");

            app.MapPost("/api/clasesprofesores/{Ci}/{IdClase}", async (IClasesProfesoresRepository repository, string Ci, int IdClase) =>
            {
                var result = await repository.AddProfesorAClase(Ci, IdClase);
                if (result)
                    return Results.NoContent();
                return Results.Problem(statusCode: 500, title: "Internal error");
            })
            .WithName("PostClaseProfesor");

            app.MapDelete("/api/clasesprofesores/{Ci}/{IdClase}", async (IClasesProfesoresRepository repository, string Ci, int IdClase) =>
            {
                var result = await repository.DeleteClaseProfesor(Ci, IdClase);
                if (result)
                    return Results.NoContent();
                return Results.Problem(statusCode: 500, title: "Internal error");
            })
            .WithName("DeleteClaseProfesor");
        }

    }
}

