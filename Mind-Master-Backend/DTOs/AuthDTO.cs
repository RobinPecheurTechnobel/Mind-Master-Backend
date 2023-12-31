using System.ComponentModel.DataAnnotations;

namespace Mind_Master_Backend.DTOs
{
    /// <summary>Information à rentrer pour se connecter</summary>
    public class AuthDTO
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }

    /// <summary>Information reçu quand un utilisateur est authentifié</summary>
    public class AuthTokenDTO
    {
        public string Token { get; set; }

        public ThinkerDTO Account { get; set; }
    }
    /// <summary>format des données nécessaire pour s'enregistrer</summary>
    public class AuthRegisterDTO
    {
        [Required]
        [MaxLength(50,ErrorMessage = "Votre login ne peut dépasser 50 caractères")]
        public string Login { get; set; }

        [Required]
        [RegularExpression("(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*\\W).{8,}$", ErrorMessage ="Entrez un mot de passe de minimum 8 caractères avec au moins une majuscule, un caractère spécial et un chiffre")]
        public string Password { get; set; }

        [Required]
        public string PasswordConfirmation { get; set; }
    }
}
