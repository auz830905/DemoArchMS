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

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. "
                                  + "\r\n\r\n Enter 'Bearer' [Space] and then your token in the text input below. "
                                  + "\r\n\r\n Example: \"Bearer 12345adcdef\"",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
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

