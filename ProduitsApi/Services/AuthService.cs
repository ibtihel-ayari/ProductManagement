using Microsoft.EntityFrameworkCore;
using ProduitsApi.Data;
using ProduitsApi.DTOs;
using ProduitsApi.Models;

namespace ProduitsApi.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly ITokenService _tokenService;

    // On injecte le DbContext ET le service de token
    public AuthService(AppDbContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    public async Task<AuthResponseDto?> RegisterAsync(RegisterDto dto)
    {
        // Vérifier que l'email n'existe pas déjà
        var existe = await _context.Utilisateurs
            .AnyAsync(u => u.Email == dto.Email);
        if (existe) return null;   // email déjà pris

        // HACHAGE du mot de passe avant stockage
        var hash = BCrypt.Net.BCrypt.HashPassword(dto.MotDePasse);

        var utilisateur = new Utilisateur
        {
            Email = dto.Email,
            MotDePasseHash = hash,
            Role = "User"
        };

        _context.Utilisateurs.Add(utilisateur);
        await _context.SaveChangesAsync();

        // On connecte directement l'utilisateur après inscription
        var (token, expiration) = _tokenService.CreerToken(utilisateur);
        return new AuthResponseDto
        {
            Token = token,
            Email = utilisateur.Email,
            Expiration = expiration
        };
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
    {
        var utilisateur = await _context.Utilisateurs
            .FirstOrDefaultAsync(u => u.Email == dto.Email);

        // Utilisateur introuvable → échec
        if (utilisateur is null) return null;

        // VÉRIFICATION du mot de passe contre le hash stocké
        var motDePasseValide = BCrypt.Net.BCrypt
            .Verify(dto.MotDePasse, utilisateur.MotDePasseHash);
        if (!motDePasseValide) return null;

        // Succès → on génère un token
        var (token, expiration) = _tokenService.CreerToken(utilisateur);
        return new AuthResponseDto
        {
            Token = token,
            Email = utilisateur.Email,
            Expiration = expiration
        };
    }
}