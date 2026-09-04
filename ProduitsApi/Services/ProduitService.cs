using Microsoft.EntityFrameworkCore;
using ProduitsApi.Data;
using ProduitsApi.DTOs;
using ProduitsApi.Models;

namespace ProduitsApi.Services;

public class ProduitService : IProduitService
{
    private readonly AppDbContext _context;

    // Le DbContext est INJECTÉ automatiquement par ASP.NET (injection de dépendances)
    public ProduitService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProduitDto>> GetAllAsync()
    {
        return await _context.Produits
            .Select(p => MapToDto(p))   // On transforme chaque entité en DTO
            .ToListAsync();
    }

    public async Task<ProduitDto?> GetByIdAsync(int id)
    {
        var produit = await _context.Produits.FindAsync(id);
        return produit is null ? null : MapToDto(produit);
    }

    public async Task<ProduitDto> CreateAsync(ProduitCreateDto dto)
    {
        var produit = new Produit
        {
            Nom = dto.Nom,
            Description = dto.Description,
            Prix = dto.Prix,
            Stock = dto.Stock,
            DateCreation = DateTime.UtcNow
        };

        _context.Produits.Add(produit);
        await _context.SaveChangesAsync();   // Écriture réelle en base
        return MapToDto(produit);
    }

    public async Task<bool> UpdateAsync(int id, ProduitCreateDto dto)
    {
        var produit = await _context.Produits.FindAsync(id);
        if (produit is null) return false;

        produit.Nom = dto.Nom;
        produit.Description = dto.Description;
        produit.Prix = dto.Prix;
        produit.Stock = dto.Stock;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var produit = await _context.Produits.FindAsync(id);
        if (produit is null) return false;

        _context.Produits.Remove(produit);
        await _context.SaveChangesAsync();
        return true;
    }

    // Méthode privée de conversion Entity → DTO (mapping)
    private static ProduitDto MapToDto(Produit p) => new()
    {
        Id = p.Id,
        Nom = p.Nom,
        Description = p.Description,
        Prix = p.Prix,
        Stock = p.Stock,
        DateCreation = p.DateCreation
    };
}