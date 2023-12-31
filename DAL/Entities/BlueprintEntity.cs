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
    [Table("Blueprint", Schema = "Idea")]
    [PrimaryKey(nameof(Id))]
    public class BlueprintEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
