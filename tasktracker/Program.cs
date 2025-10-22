using Microsoft.EntityFrameworkCore;
using tasktracker.Data;

var builder = WebApplication.CreateBuilder(args);

// On récupère la chaîne de connexion à la DB
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// On enregistre le DbContext pour l'injection de dépendances
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

// Add services to the container
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
