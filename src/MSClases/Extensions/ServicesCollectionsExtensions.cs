using System;
using Microsoft.OpenApi.Models;
using MSClases.Interfaces;
using MSClases.Services;

namespace MSClases.Extensions
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
                    Title = "Clases Microservicio",
                    Description = "Esta es la api rest para el microservicio de gestión de clases",
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

            services.AddScoped<IClasesRepository, ClasesServices>();

            services.AddScoped<IClasesProfesoresRepository, ClasesProfesoresServices>();

            #endregion
        }
    }
}

