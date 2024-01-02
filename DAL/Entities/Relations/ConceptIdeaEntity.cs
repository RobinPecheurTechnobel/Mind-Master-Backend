using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Relations
{
    [Table("ConceptIdea", Schema = "Idea")]
    [PrimaryKey(nameof(Id))]
    public class ConceptIdeaEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Concept")]
        public int ConceptId { get; set; }
        [Required]
        [ForeignKey("Idea")]
        public int IdeaId { get; set; }
        [Required]
        public uint Order { get; set; }

        public virtual ConceptEntity Concept {get; set;}
        public virtual IdeaEntity Idea { get; set; }
    }
}
