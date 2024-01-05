using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.Relations
{
    public class ConceptAssemblyModel
    {
        public int Id { get; set; }
        public uint Order { get; set; }
        public virtual ConceptModel Concept { get; set; }
    }
}
