using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.Relations
{
    public class ConceptIdeaModel
    {
        public int Id { get; set; }
        public uint Order { get; set; }
        public virtual IdeaModel Idea { get; set; }
    }
}
