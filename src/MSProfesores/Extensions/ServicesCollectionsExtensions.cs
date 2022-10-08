using Microsoft.OpenApi.Models;
using MSProfesores.Interfaces;
using MSProfesores.Services;

namespace MSProfesores.Extensions
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
                    Title = "Profesores Microservicio",
                    Description = "Esta es la api rest para el microservicio de gestión de profesores",
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

            services.AddScoped<IProfesoresRepository, ProfesoresServices>();

            #endregion
        }
    }
}

