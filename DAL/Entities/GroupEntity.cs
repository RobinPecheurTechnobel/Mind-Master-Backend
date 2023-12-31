using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("Group", Schema = "User")]
    [PrimaryKey(nameof(Id))]
    public class GroupEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Le nom du groupe ne peut dépasser 100 caractères")]
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
