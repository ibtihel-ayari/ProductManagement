using Microsoft.EntityFrameworkCore;
using ProduitsApi.Models;

namespace ProduitsApi.Data;

public class AppDbContext : DbContext
{
    // Le constructeur reçoit la configuration (via injection de dépendances)
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    // Un DbSet<T> = une table. Ici la table "Produits".
    public DbSet<Produit> Produits => Set<Produit>();

    // Optionnel : on pré-remplit quelques données de démo (seeding)
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Produit>().HasData(
            new Produit { Id = 1, Nom = "Clavier", Description = "Clavier mécanique", Prix = 79.90m, Stock = 12, DateCreation = new DateTime(2025, 1, 1) },
            new Produit { Id = 2, Nom = "Souris",  Description = "Souris sans fil",    Prix = 29.90m, Stock = 30, DateCreation = new DateTime(2025, 1, 1) },
            new Produit { Id = 3, Nom = "Écran",   Description = "Écran 27 pouces",    Prix = 199.00m, Stock = 5,  DateCreation = new DateTime(2025, 1, 1) }
        );
    }
}