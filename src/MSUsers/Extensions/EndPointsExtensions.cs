using Auth.DTOs;
using Microsoft.AspNetCore.Mvc;
using MSUsers.Interfaces;

namespace MSUsers.Extensions
{
    internal static class EndPointsExtensions
    {
        #region EndPoints
        internal static void AddEndPointsUsersExtension(this WebApplication app)
        {
            app.MapPost("/api/users", CreateUser)
            .WithName("CreateUser")
            .WithTags("Crear Usuario")
            .ProducesValidationProblem(400)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces<UserToken>(StatusCodes.Status200OK);

           app.MapPost("/api/users/login", LoginUser)
           .WithName("LoginUser")
           .WithTags("Autenticar Usuario")
           .ProducesValidationProblem(400)
           .Produces(StatusCodes.Status400BadRequest)
           .Produces<UserToken>(StatusCodes.Status200OK);            
        }
        #endregion

        #region Methods
        private static async Task<IResult> CreateUser(UserCredentials user, IUsersRepository repository, ILogger<IUsersRepository> logger)
        {
            try
            {
                if (!ValidateModel(user))
                    return Results.BadRequest(new { StatusCode = 400, Message = "El correo y contraseña son requeridos" });

                var result = await repository.CreateUser(user);

                if (result != null)
                    return Results.Ok(result);

                return Results.BadRequest(new { StatusCode = 400, Message = "Contraseña con un mínimo 8 Caracteres que incluyan mayúsculas, números y caracteres especiales" });
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "500 Internal server error");
                return Results.Problem(statusCode: 500, title: "Internal error");
            }
        }

        private static async Task<IResult> LoginUser([FromBody] UserCredentials user, IUsersRepository repository, ILogger<IUsersRepository> logger)
        {
            try
            {
                if (!ValidateModel(user))
                    return Results.BadRequest(new { StatusCode = 400, Message = "El correo y contraseña son requeridos" });

                var result = await repository.LoginUser(user);

                if (result != null)
                    return Results.Ok(result);

                return Results.BadRequest(new { StatusCode = 400, Message = "Correo o contraseña incorrectos" });
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "500 Internal server error");
                return Results.Problem(statusCode: 500, title: "Internal error");
            }
        }     

        #endregion
        private static bool ValidateModel(UserCredentials user)
        {
            if (user == null)
                return false;
            if (string.IsNullOrEmpty(user.Email))
                return false;
            if (string.IsNullOrEmpty(user.Password))
                return false;      
            return true;
        }
    }
}