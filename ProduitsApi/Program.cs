
using Microsoft.EntityFrameworkCore;
using ProduitsApi.Data;
using ProduitsApi.Services;
var builder = WebApplication.CreateBuilder(args);

// ---- 1. ENREGISTREMENT DES SERVICES (injection de dépendances) ----

builder.Services.AddControllers();

// Base de données en mémoire
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ProduitsDb"));

// Notre service métier : quand on demande IProduitService,
// ASP.NET fournit une instance de ProduitService.
builder.Services.AddScoped<IProduitService, ProduitService>();

// Swagger : documentation interactive de l'API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS : autoriser le frontend Angular (autre origine) à appeler l'API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AutoriserAngular", policy =>
        policy.WithOrigins("http://localhost:4200")  // l'URL d'Angular
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

// ---- 2. PIPELINE DE MIDDLEWARES (ordre important !) ----

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();   // Interface web : /swagger
}

app.UseCors("AutoriserAngular");   // Doit être AVANT MapControllers
app.MapControllers();

app.Run();