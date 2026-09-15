using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ProduitsApi.Models;

namespace ProduitsApi.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;

    // IConfiguration est injecté : il donne accès à appsettings.json
    public TokenService(IConfiguration config)
    {
        _config = config;
    }

    public (string token, DateTime expiration) CreerToken(Utilisateur utilisateur)
    {
        // 1. Les CLAIMS = les informations placées dans le token
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, utilisateur.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, utilisateur.Email),
            new Claim(ClaimTypes.Role, utilisateur.Role),
            // Jti = identifiant unique du token
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        // 2. La clé secrète + l'algorithme de signature
        var cle = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Cle"]!));
        var credentials = new SigningCredentials(cle, SecurityAlgorithms.HmacSha256);

        // 3. La date d'expiration
        var expiration = DateTime.UtcNow.AddMinutes(
            int.Parse(_config["Jwt:DureeMinutes"]!));

        // 4. Construction du token
        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Emetteur"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: credentials
        );

        // 5. Sérialisation en chaîne de caractères
        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return (tokenString, expiration);
    }
}