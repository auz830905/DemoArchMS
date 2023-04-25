using APIGateway.Agregators;
using APIGateway.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.Authorization;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
IConfiguration Configuration = builder.Configuration;

builder.Host.ConfigureLogging(logging => {
    logging.ClearProviders();
    logging.AddConsole();
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddCors();

builder.Services.AddAuthorizationCore();

var authenticationProviderKey = "IdentityApiKey";

builder.Services.AddAuthentication(a =>
{
    a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(authenticationProviderKey, x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["jwt:secret"])),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddOcelot()
    .AddSingletonDefinedAggregator<ClasesImpartidasPorUnProfesorAggregator>();

var app = builder.Build();

app.UseCors(cors => cors
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials()    
);

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

var configration = new OcelotPipelineConfiguration
{
    AuthorizationMiddleware = async (ctx, next) =>
    {
        if (OcelotJwtMiddleware.Authorize(ctx))
        {
            await next.Invoke();
        }
        else
        {
            ctx.Items.SetError(new UnauthorizedError($"Fail to authorize"));
        }
    }
};

app.UseOcelot().Wait();
/*Si se quiere quitar la protección o transmisión del token de acceso desde la
 api gateway hacia los micorservicios y solo validar las rutas segun los claims
basados en roles, es añadir la variable configration al metodo useOcelot*/

app.MapControllers();
app.UseAuthorization();
app.UseAuthentication();

app.Run();