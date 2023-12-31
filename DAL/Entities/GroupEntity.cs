using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
