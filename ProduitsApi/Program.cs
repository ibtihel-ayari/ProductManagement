using Microsoft.EntityFrameworkCore;
using ProduitsApi.Data;
using ProduitsApi.Services;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProduitsApi.Data;
using ProduitsApi.Services;
var builder = WebApplication.CreateBuilder(args);

// ---- 1. ENREGISTREMENT DES SERVICES (injection de dépendances) ----

builder.Services.AddControllers();

// Base de données en mémoire
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=produits.db"));

// Notre service métier : quand on demande IProduitService,
// ASP.NET fournit une instance de ProduitService.
builder.Services.AddScoped<IProduitService, ProduitService>();
builder.Services.AddScoped<IAuthService, AuthService>();       
builder.Services.AddScoped<ITokenService, TokenService>();

// --- Configuration de l'AUTHENTIFICATION JWT ---
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Que vérifie-t-on dans le token reçu ?
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,            // rejette les tokens expirés
            ValidateIssuerSigningKey = true,    // vérifie la signature

            ValidIssuer = builder.Configuration["Jwt:Emetteur"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Cle"]!))
        };
    });
builder.Services.AddAuthorization(); 

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
app.UseAuthentication();   // ← D'ABORD : QUI es-tu ? (lit le token)
app.UseAuthorization();    // ← ENSUITE : As-tu le DROIT ? (vérifie [Authorize])


app.MapControllers();

app.Run();