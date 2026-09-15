using ProduitsApi.Models;

namespace ProduitsApi.Services;

public interface ITokenService
{
    (string token, DateTime expiration) CreerToken(Utilisateur utilisateur);
}