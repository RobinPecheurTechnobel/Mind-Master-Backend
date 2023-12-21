using System.ComponentModel.DataAnnotations;

namespace Mind_Master_Backend.DTOs
{
    /// <summary>format de compte créé de l'authentification</summary>
    public class NewAccountDataTO
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    /// <summary>Forme de Compte qui souhaite recevoir du Frontend</summary>
    public class AccountDataTO
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Votre login ne peut dépasser 50 caractères")]
        public string Login { get; set; }

        [Required]
        public RoleDTO Role { get; set; }
    }

    /// <summary>Compte sous sa forme de dto (prêt à sortir vers le Frontend)</summary>
    public class AccountDTO
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public EnumDTO Role { get; set; }
    }
}
