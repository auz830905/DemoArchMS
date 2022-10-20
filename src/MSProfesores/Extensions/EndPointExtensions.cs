using Microsoft.AspNetCore.Mvc;
using MSProfesores.Interfaces;
using MSProfesores.Models;

namespace MSProfesores.Extensions
{
	internal static class EndPointExtensions
	{
        #region EndPoints
        internal static void AddEndPointsProfesoresExtension(this WebApplication app)
		{
            app.MapGet("/api/profesores/{Ci}", ObtenerProfesor)            
            .WithName("GetProfesorByCI")
            .WithTags("ObtenerProfesor")
            .RequireAuthorization()
            .ProducesValidationProblem(400)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces<Profesor>(StatusCodes.Status200OK);

            app.MapGet("/api/profesores", ListarProfesores) 
            .WithName("GetProfesores")
            .WithTags("ListarProfesores")
            .RequireAuthorization()    
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status204NoContent)
            .Produces<List<Profesor>>(StatusCodes.Status200OK);

            app.MapPost(pattern: "/api/profesores", InsertProfesor)
            .WithName("PostProfesor")
            .WithTags("AgregarProfesor")
            .RequireAuthorization()
            .ProducesValidationProblem(400)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces<Profesor>(StatusCodes.Status200OK);           

            app.MapDelete("/api/profesores/{Ci}", EliminarProfesor) 
            .WithName("DeleteProfesor")
            .WithTags("EliminarProfesor")
            .RequireAuthorization()
            .ProducesValidationProblem(400)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces<Profesor>(StatusCodes.Status200OK);

            app.MapPut("/api/profesores", ActualizarProfesor)
            .WithName("UpdateProfesor")
            .WithTags("ActualizarProfesor")
            .RequireAuthorization()
            .ProducesValidationProblem(400)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces<Profesor>(StatusCodes.Status200OK);
        }
        #endregion

        #region Methods
        private static async Task<IResult> ObtenerProfesor(string Ci, IProfesoresRepository repository, ILogger<IProfesoresRepository> logger)
        {
            try
            {
                if (!ValidateCI(Ci))
                    return Results.BadRequest(new { StatusCode = 400, Message = "Carné de identidad no válido. Tiene que tener 11 caracteres numéricos" });

                var result = await repository.GetProfesor(Ci);

                if (!string.IsNullOrEmpty(result?.CI))
                    return Results.Ok(result);

                return Results.NotFound(new { StatusCode = 404, Message = $"Profesor que desea recuperar con CI = '{Ci}' no existe" });
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "500 Internal server error");
                return Results.Problem(statusCode: 500, title: "Internal error");
            }
        }
        private static async Task<IResult> ListarProfesores(IProfesoresRepository repository, ILogger<IProfesoresRepository> logger)
        {
            try
            {
                var profesores = await repository.GetProfesores();

                if (profesores != null)
                    return Results.Ok(profesores);

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "500 Internal server error");
                return Results.Problem(statusCode: 500, title: "Internal error");
            }
        }
        private static async Task<IResult> ActualizarProfesor(Profesor profesor, IProfesoresRepository repository, ILogger<IProfesoresRepository> logger)
        {
            try
            {
                if (!ValidateCI(profesor.CI))
                    return Results.BadRequest(new { StatusCode = 400, Message = "Carné de identidad no válido. Tiene que tener 11 caracteres numéricos" });

                if (!ValidateModel(profesor))
                    return Results.BadRequest(new { StatusCode = 400, Message = "El nombre y apellidos son requeridos" });

                var result = await repository.UpdateProfesor(profesor);

                if (!string.IsNullOrEmpty(result.CI))
                    return Results.Ok(result);

                return Results.NotFound(new { StatusCode = 404, Message = $"Profesor que desea actualizar con CI = '{profesor.CI}' no existe" });
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "500 Internal server error");
                return Results.Problem(statusCode: 500, title: "Internal error");
            }
        }
        private static async Task<IResult> EliminarProfesor(string Ci, IProfesoresRepository repository, ILogger<IProfesoresRepository> logger)
        {
            try
            {
                if (!ValidateCI(Ci))
                    return Results.BadRequest("Carné de identidad no válido. Tiene que tener 11 caracteres numéricos");

                var result = await repository.DeleteProfesor(Ci);

                if (!string.IsNullOrEmpty(result.CI))
                    return Results.Ok(result);

                return Results.NotFound(new { StatusCode = 404, Message = $"El Profesor que desea eliminar con CI = '{Ci}', no existe" });
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "500 Internal server error");
                return Results.Problem(statusCode: 500, title: "Internal error");
            }
        }
        private static async Task<IResult> InsertProfesor(Profesor profesor, IProfesoresRepository repository, ILogger<IProfesoresRepository> logger)
        {
            try
            {
                if (!ValidateCI(profesor.CI))
                    return Results.BadRequest(new { StatusCode = 400, Message = "Carné de identidad no válido. Tiene que tener 11 caracteres numéricos" });

                if (!ValidateModel(profesor))
                    return Results.BadRequest(new { StatusCode = 400, Message = "El nombre y apellidos son requeridos" });

                var result = await repository.AddProfesor(profesor);

                return Results.Ok(result);                
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "500 Internal server error");
                return Results.Problem(statusCode: 500, title: "Internal error");
            }          
        }

        #endregion
        private static bool ValidateModel(Profesor profe)
        {
            if (profe == null)
                return false;
            if (string.IsNullOrEmpty(profe.CI))
                return false;
            if (profe.CI.Length != 11)
                return false;
            if (string.IsNullOrEmpty(profe.Nombre) || string.IsNullOrEmpty(profe.Apellidos))
                return false;
            return true;
        }
        private static bool ValidateCI(string CI) => (CI.Length == 11);        
    }
}