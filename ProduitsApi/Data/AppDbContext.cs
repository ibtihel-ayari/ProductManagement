using Microsoft.EntityFrameworkCore;
using ProduitsApi.Models;

namespace ProduitsApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Produit> Produits => Set<Produit>();
    public DbSet<Utilisateur> Utilisateurs => Set<Utilisateur>();   // ← NOUVEAU

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // L'email doit être unique (pas deux comptes avec le même email)
        modelBuilder.Entity<Utilisateur>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // Le seeding des produits (dates fixes, cf. tuto précédent)
        modelBuilder.Entity<Produit>().HasData(
            new Produit { Id = 1, Nom = "Clavier", Description = "Clavier mécanique", Prix = 79.90m, Stock = 12, DateCreation = new DateTime(2025, 1, 1) },
            new Produit { Id = 2, Nom = "Souris",  Description = "Souris sans fil",    Prix = 29.90m, Stock = 30, DateCreation = new DateTime(2025, 1, 1) },
            new Produit { Id = 3, Nom = "Écran",   Description = "Écran 27 pouces",    Prix = 199.00m, Stock = 5,  DateCreation = new DateTime(2025, 1, 1) }
        );
    }
}