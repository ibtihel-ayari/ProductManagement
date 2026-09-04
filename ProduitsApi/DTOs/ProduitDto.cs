namespace ProduitsApi.DTOs;

public class ProduitDto
{
    public int Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Prix { get; set; }
    public int Stock { get; set; }
    public DateTime DateCreation { get; set; }
}