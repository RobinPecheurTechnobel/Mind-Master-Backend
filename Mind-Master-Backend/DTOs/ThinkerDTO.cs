using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Mind_Master_Backend.DTOs
{
    /// <summary>format de compte créé de l'authentification</summary>
    public class NewAccountDataTO
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    /// <summary>Forme de Compte qui souhaite recevoir du Frontend</summary>
    public class ThinkerDataTO
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Votre login ne peut dépasser 50 caractères")]
        public string Login { get; set; }

        [Required]
        public RoleDTO Role { get; set; }

        [AllowNull]
        [RegularExpression(@"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$")]
        public string? Email { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Le pseudo ne peut dépasser 100 caractères")]
        public string Pseudo { get; set; }
    }

    /// <summary>Compte sous sa forme de dto (prêt à sortir vers le Frontend)</summary>
    public class ThinkerFullDTO : ThinkerDTO
    {

        public IEnumerable<GroupThinkerInThinkerDTO> Groups { get; set; }

    }
    public class ThinkerDTO
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public EnumDTO Role { get; set; }
        public string Pseudo { get; set; }
        public string? Email { get; set; }

    }
    public class GroupThinkerInThinkerDTO
    {
        public GroupDTO Group { get; set; }
        public bool isOwner { get; set; }
    }

}
