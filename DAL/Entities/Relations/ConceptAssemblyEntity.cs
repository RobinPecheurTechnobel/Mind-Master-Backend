using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Entities.Relations
{
    [Table("ConceptAssembly", Schema = "Idea")]
    [PrimaryKey(nameof(Id))]
    public class ConceptAssemblyEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Assembly")]
        public int AssemblyId { get; set; }
        [Required]
        [ForeignKey("Concept")]
        public int ConceptId { get; set; }
        [Required]
        public uint Order { get; set; }

        public virtual AssemblyEntity Assembly { get; set; }
        public virtual IEnumerable<ConceptIdeaEntity> ConceptIdeas { get; set; }
    }
}
