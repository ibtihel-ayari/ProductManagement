namespace ProduitsApi.Models;

// Une entité = une table en base de données.
// Chaque propriété = une colonne.
public class Produit
{
    public int Id { get; set; }              // Clé primaire (auto-incrémentée)
    public string Nom { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Prix { get; set; }
    public int Stock { get; set; }
    public DateTime DateCreation { get; set; } = DateTime.UtcNow;
}