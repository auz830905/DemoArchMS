using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MSClases.Extensions;
using MSClases.Models;

var builder = WebApplication.CreateBuilder(args);
IConfiguration Configuration = builder.Configuration;

builder.Host.ConfigureLogging(logging => {
    logging.ClearProviders();
    logging.AddConsole();
});

builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddSwaggerExtension();
builder.Services.AddInfraestructure();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(
    options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["jwt:secret"])),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddDbContext<DBContextClases>(options =>
{
    var host = Configuration.GetValue<string>("ConnectionStrings:host")!.ToString();
    var port = Configuration.GetValue<string>("ConnectionStrings:port")!.ToString();
    var database = Configuration.GetValue<string>("ConnectionStrings:database")!.ToString();
    var user = Configuration.GetValue<string>("ConnectionStrings:user")!.ToString();
    var password = Configuration.GetValue<string>("ConnectionStrings:password")!.ToString();

    var connectonString = $"Host={host}:{port};Database={database}; Username={user};Password={password}";

    options.UseNpgsql(connectonString);
});

//builder.Services.AddEntityFrameworkSqlServer().AddDbContext<DBContextClases>(options =>
//{
   //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionSQLServer"));
//});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<DBContextClases>();
    //dataContext.Database.Migrate();
    dataContext.Database.EnsureDeleted();
    dataContext.Database.EnsureCreated();
}

app.UseCors(cors => cors
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials()
);

app.ConfigureSwagger();
app.UseHttpsRedirection();
app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.AddEndPointsClasesExtension();
app.AddEndPointClasesProfesoresExtensions();

app.Run();