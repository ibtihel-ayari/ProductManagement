using ProduitsApi.DTOs;

namespace ProduitsApi.Services;

public interface IProduitService
{
    Task<IEnumerable<ProduitDto>> GetAllAsync();
    Task<ProduitDto?> GetByIdAsync(int id);
    Task<ProduitDto> CreateAsync(ProduitCreateDto dto);
    Task<bool> UpdateAsync(int id, ProduitCreateDto dto);
    Task<bool> DeleteAsync(int id);
}