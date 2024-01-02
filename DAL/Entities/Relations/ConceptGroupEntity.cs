using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Relations
{
    [Table("ConceptGroup", Schema = "Idea")]
    [PrimaryKey(nameof(ConceptId), nameof(GroupId))]
    public class ConceptGroupEntity
    {
        [ForeignKey("Concept")]
        public int ConceptId { get; set; }
        [ForeignKey("Group")]
        public int GroupId { get; set; }
    }
}
