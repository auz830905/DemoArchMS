using MSClases.Interfaces;
using MSClases.Models;

namespace MSClases.Extensions
{
    internal static class EndPointsClasesProfesoresExtensions
    {
        internal static void AddEndPointClasesProfesoresExtensions(this WebApplication app)
        {
            app.MapGet("/api/clasesprofesores/{Ci}", ListarClasesPorProfesor) 
            .WithName("GetClasesByProfesorId")
            .WithTags("ObtenerProfesor")
            //.RequireAuthorization()
            .ProducesValidationProblem(400)
            //.Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces<List<Clase>>(StatusCodes.Status200OK);

            app.MapGet("/api/clasesprofesores/{Ci}/clasesnotassign", ListarClasesNoAsiganadasAProfesor) 
            .WithName("GetClasesNotAsignedProfesorId")
            .WithTags("ObtenerClasesNoAsignadasAProfesor")
            //.RequireAuthorization()
            .ProducesValidationProblem(400)
            //.Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces<List<Clase>>(StatusCodes.Status200OK);

            app.MapPost("/api/clasesprofesores/{Ci}/{IdClase}", AgregarClaseAProfesor)
            .WithName("PostClaseProfesor")
            .WithTags("EliminarClasesAProfesor")
            //.RequireAuthorization()
            .ProducesValidationProblem(400)
            //.Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces<ClaseProfesor>(StatusCodes.Status200OK);

            app.MapDelete("/api/clasesprofesores/{Ci}/{IdClase}", EliminarClaseAProfesor) 
            .WithName("DeleteClaseProfesor")
            .WithTags("EliminarClaseAProfesor")
            //.RequireAuthorization()
            .ProducesValidationProblem(400)
            //.Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces<ClaseProfesor>(StatusCodes.Status200OK);

            app.MapDelete("/api/clasesprofesores/{Ci}", EliminarTodasLasClaseAProfesor) 
            .WithName("DeleteAllClasesAProfesor")
            .WithTags("EliminarTodasLasClaseAProfesor")
            //.RequireAuthorization()
            .ProducesValidationProblem(400)
            //.Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces<ClaseProfesor>(StatusCodes.Status200OK);
        }

        #region Methods ClasesProfesores
        private static async Task<IResult> ListarClasesNoAsiganadasAProfesor(string Ci, IClasesProfesoresRepository repository, ILogger<IClasesRepository> logger)
        {
            try
            {
                if (!ValidateCI(Ci))
                    return Results.BadRequest("Carné de identidad no válido. Tiene que tener 11 caracteres numéricos");

                var clases = await repository.GetClasesNotAsinadasAProfesor(Ci);

                if (clases != null)
                    return Results.Ok(clases);

                return Results.NotFound("El Carné de identidad '{Ci}' no existe");
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "500 Internal server error");
                return Results.Problem(statusCode: 500, title: "Internal error");
            }
        }

        private static async Task<IResult> ListarClasesPorProfesor(string Ci, IClasesProfesoresRepository repository, ILogger<IClasesRepository> logger)
        {
            try
            {
                if (!ValidateCI(Ci))
                    return Results.BadRequest("Carné de identidad no válido. Tiene que tener 11 caracteres numéricos");

                var clases = await repository.GetClasesByProfesor(Ci);

                if (clases != null)
                    return Results.Ok(clases);

                return Results.NotFound("El Carné de identidad '{Ci}' no existe");
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "500 Internal server error");
                return Results.Problem(statusCode: 500, title: "Internal error");
            }
        }
        private static async Task<IResult> AgregarClaseAProfesor(string Ci, int IdClase, IClasesProfesoresRepository repository, ILogger<IClasesRepository> logger)
        {
            try
            {
                if (!ValidateCI(Ci))
                    return Results.BadRequest("Carné de identidad no válido. Tiene que tener 11 caracteres numéricos");

                var claseProfesor = await repository.AddProfesorAClase(Ci, IdClase);

                if (claseProfesor != null)
                    return Results.Ok(claseProfesor);

                return Results.NotFound("El clase que intenta asignar no existe");
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "500 Internal server error");
                return Results.Problem(statusCode: 500, title: "Internal error");
            }
        }

        private static async Task<IResult> EliminarClaseAProfesor(string Ci, int IdClase, IClasesProfesoresRepository repository, ILogger<IClasesRepository> logger)
        {
            try
            {
                if (!ValidateCI(Ci))
                    return Results.BadRequest("Carné de identidad no válido. Tiene que tener 11 caracteres numéricos");

                var claseProfesor = await repository.DeleteClaseProfesor(Ci, IdClase);

                if (claseProfesor != null)
                    return Results.Ok(claseProfesor);

                return Results.NotFound("El clase que intenta eliminar no existe");
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "500 Internal server error");
                return Results.Problem(statusCode: 500, title: "Internal error");
            }
        }

        private static async Task<IResult> EliminarTodasLasClaseAProfesor(string Ci, IClasesProfesoresRepository repository, ILogger<IClasesRepository> logger)
        {
            try
            {
                if (!ValidateCI(Ci))
                    return Results.BadRequest("Carné de identidad no válido. Tiene que tener 11 caracteres numéricos");

                var result = await repository.DeleteClaseProfesor(Ci);

                if (result)
                    return Results.Ok();

                return Results.NotFound($"Al CI = {Ci} que intenta elminar las clases no existe");
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "500 Internal server error");
                return Results.Problem(statusCode: 500, title: "Internal error");
            }
        }
        #endregion
        private static bool ValidateCI(string CI) => (CI.Length == 11);
    }
}