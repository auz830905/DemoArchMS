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
    var host = Configuration.GetValue<string>("ConnectionStrings:host")!.ToString();
    var port = Configuration.GetValue<string>("ConnectionStrings:port")!.ToString();
    var database = Configuration.GetValue<string>("ConnectionStrings:database")!.ToString();
    var user = Configuration.GetValue<string>("ConnectionStrings:user")!.ToString();
    var password = Configuration.GetValue<string>("ConnectionStrings:password")!.ToString();

    var connectionString = $"server={host}; port={port}; database={database}; user={user}; password={password}; Persist Security Info=False; Connect Timeout=300";
    
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
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