using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json.Serialization;
using tasktracker.Data;
using tasktracker.Repositories;
using tasktracker.Services;

var builder = WebApplication.CreateBuilder(args);

// On récupère la chaîne de connexion à la DB
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// On enregistre le DbContext pour l'injection de dépendances
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

// Add services to the container
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

// Enregistrement des repositories
builder.Services.AddScoped<ICommonRepository, CommonRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Enregistrement des services
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

app.UseExceptionHandler("/error");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
