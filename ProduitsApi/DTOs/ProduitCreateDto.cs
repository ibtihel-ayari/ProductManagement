using System.ComponentModel.DataAnnotations;

namespace ProduitsApi.DTOs;

public class ProduitCreateDto
{
    // Les attributs de validation vérifient les données AVANT traitement
    [Required(ErrorMessage = "Le nom est obligatoire")]
    [StringLength(100, MinimumLength = 2)]
    public string Nom { get; set; } = string.Empty;

    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    [Range(0, 100000, ErrorMessage = "Le prix doit être positif")]
    public decimal Prix { get; set; }

    [Range(0, int.MaxValue)]
    public int Stock { get; set; }
}