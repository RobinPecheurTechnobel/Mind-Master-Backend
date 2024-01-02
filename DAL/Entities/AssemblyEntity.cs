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
    [Table("Assembly", Schema = "Idea")]
    [PrimaryKey(nameof(Id))]
    public class AssemblyEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
}
