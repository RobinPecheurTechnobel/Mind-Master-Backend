using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Relations
{
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
    }
}
