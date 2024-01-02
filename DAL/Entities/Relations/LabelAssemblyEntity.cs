using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Relations
{
    [Table("LabelAssembly", Schema = "Idea")]
    [PrimaryKey(nameof(LabelId), nameof(AssemblyId))]
    public class LabelAssemblyEntity
    {
        [ForeignKey("Label")]
        public int LabelId { get; set; }
        [ForeignKey("Assembly")]
        public int AssemblyId { get; set; }
    }
}
