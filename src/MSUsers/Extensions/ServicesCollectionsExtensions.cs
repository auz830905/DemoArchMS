using Microsoft.OpenApi.Models;
using MSUsers.Interfaces;
using MSUsers.Services;

namespace MSUsers.Extensions
{
    internal static class ServicesCollectionsExtensions
    {
        internal static void AddSwaggerExtension(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "Users Microservicio",
                    Description = "Esta es la api rest para el microservicio de gestión de usuarios. Proporciona token de autenticacion",
                    Contact = new OpenApiContact()
                    {
                        Name = "Dev. Arley Ulloa Zaila"
                    }
                });

            });
        }
        internal static void AddInfraestructure(this IServiceCollection services)
        {
            #region Repositories

            services.AddScoped<IUsersRepository, UsersServices>();

            #endregion
        }
    }
}