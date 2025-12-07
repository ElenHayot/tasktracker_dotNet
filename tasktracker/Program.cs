using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using tasktracker.Data;
using tasktracker.Repositories;
using tasktracker.Services;

var builder = WebApplication.CreateBuilder(args);

#region Proxy configuration
// To catch real IP and not reverse proxy IPs
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;

    // Adds reverse proxy's internal IPs
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});
#endregion

#region CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// CORS authorized htpp/https addresses
var allowedOrigins = new[]
{
    "https://localhost:17807",
    "https://localhost:7190",
    "http://localhost:5093",
    "http://localhost:5173",
};

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy
                .WithOrigins(allowedOrigins)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});
#endregion

#region JWT authorization
// Récupère la section "Jwt" décrite dans appsettings.Development
var jwtSettings = builder.Configuration.GetSection("Jwt");

builder.Services
    .AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
        };
    });

builder.Services.AddAuthorization();
#endregion

#region Loading context
// On récupère la chaîne de connexion à la DB
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// On enregistre le DbContext pour l'injection de dépendances
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));
#endregion

#region Add controllers
// Add controllers to the container
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
#endregion

#region Swagger / OpenAPI
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});
#endregion

#region Add repositories
// Enregistrement des repositories
builder.Services.AddScoped<ICommonRepository, CommonRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
#endregion

#region Add services
// Enregistrement des services métiers
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();

// Enregistrement des services tâches de fond
builder.Services.AddHostedService<TokenCleanupService>();
#endregion

var app = builder.Build();

app.UseExceptionHandler("/error");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseForwardedHeaders();
//app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
