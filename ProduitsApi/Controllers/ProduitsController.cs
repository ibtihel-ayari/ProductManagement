using Microsoft.AspNetCore.Mvc;
using ProduitsApi.DTOs;
using ProduitsApi.Services;

namespace ProduitsApi.Controllers;

[ApiController]                       // Active la validation auto + le binding JSON
[Route("api/[controller]")]          // → route de base : /api/produits
public class ProduitsController : ControllerBase
{
    private readonly IProduitService _service;

    // On injecte l'INTERFACE, pas la classe concrète (couplage faible)
    public ProduitsController(IProduitService service)
    {
        _service = service;
    }

    // GET /api/produits
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProduitDto>>> GetAll()
    {
        var produits = await _service.GetAllAsync();
        return Ok(produits);          // 200 OK + la liste JSON
    }

    // GET /api/produits/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProduitDto>> GetById(int id)
    {
        var produit = await _service.GetByIdAsync(id);
        if (produit is null)
            return NotFound();        // 404
        return Ok(produit);           // 200
    }

    // POST /api/produits
    [HttpPost]
    public async Task<ActionResult<ProduitDto>> Create(ProduitCreateDto dto)
    {
        // Grâce à [ApiController], si dto est invalide → 400 automatique ici
        var cree = await _service.CreateAsync(dto);
        // 201 Created + l'URL de la nouvelle ressource
        return CreatedAtAction(nameof(GetById), new { id = cree.Id }, cree);
    }

    // PUT /api/produits/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProduitCreateDto dto)
    {
        var ok = await _service.UpdateAsync(id, dto);
        if (!ok) return NotFound();
        return NoContent();           // 204 (succès, rien à renvoyer)
    }

    // DELETE /api/produits/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _service.DeleteAsync(id);
        if (!ok) return NotFound();
        return NoContent();           // 204
    }
}