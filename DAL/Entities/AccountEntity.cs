using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("Account", Schema = "Account")]
    [PrimaryKey(nameof(Id))]
    public class AccountEntity
    {
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
    }
}
