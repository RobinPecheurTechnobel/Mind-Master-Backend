using System.ComponentModel.DataAnnotations;

namespace Mind_Master_Backend.DTOs
{
    public class AuthDTO
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
    public class AuthTokenDTO
    {
        public string Token { get; set; }

        public AccountDTO Account { get; set; }
    }
    public class AuthRegisterDTO
    {
        [Required]
        public string Login { get; set; }

        [Required]
        [RegularExpression("(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*\\W).{8,}$", ErrorMessage ="Entrez un mot de passe de minimum 8 caractères avec au moins une majuscule, un caractère spécial et un chiffre")]
        public string Password { get; set; }

        [Required]
        public string PasswordConfirmation { get; set; }
    }
}
