using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DAL.Entities.Relations
{
    [Table("LabelConcept", Schema = "Idea")]
    [PrimaryKey(nameof(LabelId), nameof(ConceptId))]
    public class LabelConceptEntity
    {
        [ForeignKey("Label")]
        public int LabelId { get; set; }
        [ForeignKey("Concept")]
        public int ConceptId { get; set; }
    }
}
