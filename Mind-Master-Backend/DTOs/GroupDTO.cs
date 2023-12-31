using BLL.Models;
using System.ComponentModel.DataAnnotations;

namespace Mind_Master_Backend.DTOs
{
    public class GroupFullDTO : GroupDTO
    {
        public IEnumerable<GroupThinkerInGroupDTO> Thinkers { get; set; }
    }

    public class GroupDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? Description { get; set; }
    }
    public class GroupThinkerInGroupDTO
    {
        public ThinkerDTO Thinker { get; set; }
        public bool isOwner { get; set; }
    }

    public class GroupDataToObject
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Le nom du groupe ne peut dépasser 100 caractères")]
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
