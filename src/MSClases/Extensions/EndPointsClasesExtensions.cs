using Microsoft.AspNetCore.Mvc;
using MSClases.Interfaces;
using MSClases.Models;

namespace MSClases.Extensions
{
	internal static class EndPointsClasesExtensions
	{
        internal static void AddEndPointsClasesExtension(this WebApplication app)
        {
            app.MapGet("/api/clases/{Id}", ObtenerClase)
            .WithName("GetClasesById")
            .WithTags("Gestión de clases")
            .RequireAuthorization()
            .ProducesValidationProblem(400)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound)
            .Produces<Clase>(StatusCodes.Status200OK);

            app.MapGet("/api/clases", ListarClases)
            .WithName("GetClases")
            .WithTags("Gestión de clases")
            .RequireAuthorization()    
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status204NoContent)
            .Produces<List<Clase>>(StatusCodes.Status200OK);

            app.MapPost("/api/clases", InsertClase)
            .WithName("PostClase")
            .WithTags("Gestión de clases")
            .RequireAuthorization()
            .ProducesValidationProblem(400)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces<Clase>(StatusCodes.Status200OK);

            app.MapDelete("/api/clases/{Id}", EliminarClase) 
            .WithName("DeleteClase")
            .WithTags("Gestión de clases")
            .RequireAuthorization()
            .ProducesValidationProblem(400)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound)
            .Produces<Clase>(StatusCodes.Status200OK);

            app.MapPut("/api/clases", ActualizarClase)
            .WithName("UpdateClase")
            .WithTags("Gestión de clases")
            .RequireAuthorization()
            .ProducesValidationProblem(400)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces<Clase>(StatusCodes.Status200OK);
        }
                
        #region Methods Clases
        private static async Task<IResult> ObtenerClase(int Id, IClasesRepository repository, ILogger<IClasesRepository> logger)
        {
            try
            {
                var clase = await repository.GetClase(Id);

                if (clase != null)
                    return Results.Ok(clase);

                return Results.NotFound(new { StatusCode = 404, Message = "Clase no encontrada" });
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "500 Internal server error");
                return Results.Problem(statusCode: 500, title: "Internal error");
            }
        }
        private static async Task<IResult> ListarClases(IClasesRepository repository, ILogger<IClasesRepository> logger)
        {
            try
            {
                var clases = await repository.GetClases();

                if (clases != null)
                    return Results.Ok(clases);

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "500 Internal server error");
                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        private static async Task<IResult> ActualizarClase(Clase clase, IClasesRepository repository, ILogger<IClasesRepository> logger)
        {
            try
            {
                if (InvalidateModel(clase))
                    return Results.BadRequest(new { StatusCode = 400, Message = "El nombre de la clase es requerido" });

                var result = await repository.UpdateClase(clase);

                if (result != null)
                    return Results.Ok(result);

                return Results.NotFound(new { StatusCode = 404, Message = $"La clase que desea actualizar no existe" });
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "500 Internal server error");
                return Results.Problem(statusCode: 500, title: "Internal error");
            }
        }
        private static async Task<IResult> EliminarClase(int Id, IClasesRepository repository, ILogger<IClasesRepository> logger)
        {
            try
            {
                var clase = await repository.DeleteClase(Id);

                if (clase != null)
                    return Results.Ok(clase);

                return Results.NotFound(new { StatusCode = 404, Message = $"La clase que intenta eliminar no existe" });
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "500 Internal server error");
                return Results.Problem(statusCode: 500, title: "Internal error");
            }
        }
        private static async Task<IResult> InsertClase([FromBody] Clase clase, IClasesRepository repository, ILogger<IClasesRepository> logger)
        {
            try
            {
                if (InvalidateModel(clase))
                    return Results.BadRequest(new { StatusCode = 400, Message = "El nombre de la clase es requerido" });

                var result = await repository.AddClase(clase);

                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "500 Internal server error");
                return Results.Problem(statusCode: 500, title: "Internal error");
            }
        }
        #endregion              

        private static bool InvalidateModel(Clase clase)
        {            
            return string.IsNullOrEmpty(clase.Nombre);
        }        
    }
}