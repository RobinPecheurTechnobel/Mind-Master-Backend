using DAL.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DAL.Entities
{
    [Table("Thinker", Schema = "User")]
    [PrimaryKey(nameof(Id))]
    public class ThinkerEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Le Login ne peut dépasser 50 caractères")]
        public string Login { get; set; }


        public string HashPassword { get; set; }

        [Required]
        [MaxLength(10)]
        public string Role
        {
            get
            {
                return role.ToString();
            }
            set
            {
                if(!Enum.TryParse<RoleEntity>(value,out RoleEntity result )) 
                {
                    role = 0;
                }
                role = result;
            }
        }
        [NotMapped]
        public RoleEntity role { get; set; }


        [AllowNull]
        public string? Email { get; set; }

        [MaxLength(100, ErrorMessage = "Le pseudo ne peut dépasser 100 caractères")]
        public string Pseudo { get; set; }
    }
}
