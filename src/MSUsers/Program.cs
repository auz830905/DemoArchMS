using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MSUsers.Extensions;
using MSUsers.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
IConfiguration Configuration = builder.Configuration;

builder.Host.ConfigureLogging(logging => {
    logging.ClearProviders();
    logging.AddConsole();
});

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddCors();
builder.Services.AddSwaggerExtension();
builder.Services.AddInfraestructure();

builder.Services.AddDbContext<DBContextUsers>(options =>
{
    var connections = Configuration.GetConnectionString("DefaultConnectionMySQL");
    options.UseMySql(connections, ServerVersion.AutoDetect(connections));
});

//builder.Services.AddEntityFrameworkSqlServer().AddDbContext<DBContextUsers>(options =>
//{
    //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionSQLServer"));
//});

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<DBContextUsers>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["jwt:secret"])),
            ClockSkew = TimeSpan.Zero
        });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<DBContextUsers>();
    //dataContext.Database.Migrate();
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.AddEndPointsUsersExtension();

app.Run();