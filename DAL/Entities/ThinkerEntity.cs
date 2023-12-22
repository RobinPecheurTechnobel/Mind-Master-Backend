using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("Thinker", Schema = "User")]
    [PrimaryKey(nameof(Id))]
    public class ThinkerEntity
    {
        public int Id { get; set; }

        [AllowNull]
        [MaxLength(100, ErrorMessage = "Le nom de famille ne peut dépasser 100 caractères")]
        public string? LastName { get; set; }

        [AllowNull]
        [MaxLength(100, ErrorMessage = "Le prénom ne peut dépasser 100 caractères")]
        public string? FirstName { get; set; }

        [AllowNull]
        public string? Email { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Le pseudo ne peut dépasser 100 caractères")]
        public string Pseudo { get; set; }
    }
}
