namespace ProduitsApi.Models;

public class Utilisateur
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;

    // ⚠️ On ne stocke JAMAIS le mot de passe en clair.
    // On stocke uniquement son "hash" (empreinte irréversible).
    public string MotDePasseHash { get; set; } = string.Empty;

    public string Role { get; set; } = "User";   // pour gérer les permissions plus tard
    public DateTime DateInscription { get; set; } = DateTime.UtcNow;
}